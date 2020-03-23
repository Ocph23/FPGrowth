using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using MainWebApp.Models;
using MainWebApp.Models.Data;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace MainWebApp.Services {
    public interface IUserService {
        User Authenticate (string username, string password);
        bool verifyemail (int userid, string token);
    }

    public class UserService : IUserService {

        private readonly AppSettings _appSettings;
        private readonly OcphDbContext db;

        public UserService (IOptions<AppSettings> appSettings, OcphDbContext _db) {
            _appSettings = appSettings.Value;
            db = _db;
        }

        public User Authenticate (string username, string password) {
            try {
                var user = db.Users.Where (x => x.username == username).FirstOrDefault ();

                //var user = _users.SingleOrDefault(x => x.Username == username && x.Password == password);

                // return null if user not found
                if (user == null) {
                    RegisterAdmin ();
                    throw new SystemException ("Anda Tidak Memiliki Akses");
                }

                if (user.emailconfirm==0)
                    throw new SystemException ("Menunggu Verifikasi Account");

                if (!Helper.VerifyMd5Hash (password, user.password))
                    throw new SystemException ("Anda Tidak Memiliki Akses");

                user.Token = user.GenerateToken (_appSettings.Secret);
                return user;
            } catch (System.Exception ex) {

                throw new SystemException (ex.Message);
            }
        }

        public bool verifyemail (int userid, string token) {
            try {
                var user = db.Users.Where (x => x.iduser == userid).FirstOrDefault ();
                if (user == null)
                    throw new SystemException ("Anda Tidak Memiliki Akses");

                if (user.emailconfirm ==1)
                    throw new SystemException ("Email telah diverifikasi");

                var aktif = db.Users.Update (x => new { x.emailconfirm }, new User { emailconfirm = 1, iduser = userid}, x => x.iduser == userid);
                if (!aktif)
                    throw new SystemException ("Terjadi Kesalahan, Silahkan Riset ");
                return true;

            } catch (System.Exception) {

                throw;
            }
        }

        private Task RegisterAdmin () {
            var transaction = db.BeginTransaction ();
            try {

                var users = db.Users.Select ();
                if (users.Count () <= 0) {
                    var user = new User { username = "admin", password = Helper.GetMd5Hash ("admin"),    role="AdminSuper", email ="admin@gmail.com"};
                    user = user.CreateUser (db);
                   var token = user.GenerateToken (_appSettings.Secret);
                    var emailService = new EmailService ();
                    emailService.SendEmail ("ocph23@gmail.com", $"<a href='https://localhost:5001/user/verifyemail?userid={user.iduser}&token={token}'>Verifikasi Accunt</a>");
                    transaction.Commit ();
                }
                return Task.CompletedTask;

            } catch (System.Exception ex) {
                transaction.Rollback ();
                return Task.CompletedTask;
            }
        }
    }

    public static class UserExtention {
        public static User CreateUser (this User user, OcphDbContext db) {
            user.iduser = db.Users.InsertAndGetLastID (user);
            return user;
        }
     
        public static User GetDataUser (this System.Security.Claims.ClaimsPrincipal user, OcphDbContext db) {
            var claim = user.Claims.Where (x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault ();
            var result = db.Users.Where (x => x.username == claim.Value).FirstOrDefault ();
            return result;
        }

        public static string GenerateToken (this User user, string secretCode) {
            var claims = new List<Claim> {
                new Claim (JwtRegisteredClaimNames.Sub, user.username),
                new Claim (JwtRegisteredClaimNames.Jti, Guid.NewGuid ().ToString ()),
                new Claim (JwtRegisteredClaimNames.NameId, user.iduser.ToString ()),
                new Claim (ClaimTypes.NameIdentifier, user.iduser.ToString ())
            };

            var key = new SymmetricSecurityKey (Encoding.UTF8.GetBytes (secretCode));
            var creds = new SigningCredentials (key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays (Convert.ToDouble (7));

            var token = new JwtSecurityToken (
                secretCode,
                secretCode,
                claims,
                expires : expires,
                signingCredentials : creds
            );

            return new JwtSecurityTokenHandler ().WriteToken (token);
        }
    }

}