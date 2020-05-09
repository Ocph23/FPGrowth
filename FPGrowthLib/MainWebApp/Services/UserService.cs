using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using MainWebApp.Models;
using MainWebApp.Models.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace MainWebApp.Services {
    public interface IUserService {
        User Authenticate (string username, string password);
        bool verifyemail (int userid, string token);
        object profile (int userid);
        Task<Pembeli> RegisterPembeli (Pembeli pembeli);
        Task<Penjual> RegisterPenjual (Penjual pembeli);
        Task<User> UpdatePhotoProfile (int id, byte[] model);
        Task<bool> changeStatusPembeli (int userId);
        Task<bool> changeStatusPenjual (int userId);
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

                var user = db.Users.Where (x => x.username == username || x.email == username).FirstOrDefault ();

                //var user = _users.SingleOrDefault(x => x.Username == username && x.Password == password);

                // return null if user not found
                if (user == null) {
                    RegisterAdmin ();
                    throw new SystemException ("Anda Tidak Memiliki Akses");
                }

                if (user.emailconfirm == 0)
                    throw new SystemException ("Menunggu Verifikasi Account");

                if (!Helper.VerifyMd5Hash (password, user.password))
                    throw new SystemException ("Anda Tidak Memiliki Akses");

                user.Token = user.GenerateToken (_appSettings.Secret);
                return user;
            } catch (System.Exception ex) {

                throw new SystemException (ex.Message);
            }
        }

        public object profile (int userid) {
            var user = db.Users.Where (x => x.iduser == userid).FirstOrDefault ();
            return user.profile (db);
        }

        public bool verifyemail (int userid, string token) {
            try {
                var user = db.Users.Where (x => x.iduser == userid).FirstOrDefault ();
                if (user == null)
                    throw new SystemException ("Anda Tidak Memiliki Akses");

                if (user.emailconfirm == 1)
                    throw new SystemException ("Email telah diverifikasi");

                var aktif = db.Users.Update (x => new { x.emailconfirm }, new User { emailconfirm = 1, iduser = userid }, x => x.iduser == userid);
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
                    var user = new User { username = "admin", password = Helper.GetMd5Hash ("admin"), role = "adminsuper", email = "admin@gmail.com" };
                    user = user.CreateUser (db);
                    var token = user.GenerateToken (_appSettings.Secret);
                    var emailService = new EmailService ();

                    emailService.SendEmail ("ocph23@gmail.com", getEmailMessage (user.iduser, token));
                    transaction.Commit ();
                }
                return Task.CompletedTask;

            } catch (System.Exception) {
                transaction.Rollback ();
                return Task.CompletedTask;
            }
        }

        public Task<Pembeli> RegisterPembeli (Pembeli pembeli) {
            var transaction = db.BeginTransaction ();
            try {
                var user = new User { username = pembeli.email_pembeli, password = Helper.GetMd5Hash (pembeli.Password), role = "pembeli", email = pembeli.email_pembeli };
                user = user.CreateUser (db);
                pembeli.iduser = user.iduser;
                pembeli.tgl_daftar = DateTime.Now;
                pembeli.status = true;
                pembeli.idpembeli = db.Pembeli.InsertAndGetLastID (pembeli);
                if (pembeli.idpembeli <= 0)
                    throw new SystemException ("Registrasi Gagal");

                var admin = db.Users.Where (x => x.role == "Adminsuper").FirstOrDefault ();
                if (admin != null) {
                    Pesan pesan = new Pesan { tgl_pesan = DateTime.Now, pengirim = admin.username, idpenerima = user.iduser, idpengirim = admin.iduser, isi_pesan = "Selamat Datang" };
                    db.Chat.Insert (pesan);

                }

                var token = user.GenerateToken (_appSettings.Secret);
                var emailService = new EmailService ();
                emailService.SendEmail (pembeli.email_pembeli, getEmailMessage (user.iduser, token));
                transaction.Commit ();
                return Task.FromResult (pembeli);

            } catch (System.Exception ex) {
                transaction.Rollback ();
                throw new SystemException (ex.Message);
            }
        }

        public Task<Penjual> RegisterPenjual (Penjual penjual) {
            var transaction = db.BeginTransaction ();
            try {
                var user = new User {
                    username = penjual.email, password = Helper.GetMd5Hash (penjual.Password),
                    role = "penjual", email = penjual.email
                };
                user = user.CreateUser (db);
                penjual.iduser = user.iduser;
                penjual.status = true;
                penjual.tgl_daftar = DateTime.Now;
                penjual.idpenjual = db.Penjual.InsertAndGetLastID (penjual);
                if (penjual.idpenjual <= 0)
                    throw new SystemException ("Registrasi Gagal");

                var admin = db.Users.Where (x => x.role == "Adminsuper").FirstOrDefault ();
                if (admin != null) {
                    Pesan pesan = new Pesan { tgl_pesan = DateTime.Now, pengirim = admin.username, idpenerima = user.iduser, idpengirim = admin.iduser, isi_pesan = "Selamat Datang" };
                    db.Chat.Insert (pesan);

                }

                var token = user.GenerateToken (_appSettings.Secret);
                var emailService = new EmailService ();
                emailService.SendEmail (penjual.email, getEmailMessage (user.iduser, token));
                transaction.Commit ();
                return Task.FromResult (penjual);

            } catch (System.Exception ex) {
                transaction.Rollback ();
                throw new SystemException (ex.Message);
            }
        }

        private string getEmailMessage (int iduser, string token) {
            string baseUrl = AppDomain.CurrentDomain.GetData ("BaseUrl").ToString ();
            return $@"<h1><span style='color: #00ccff;'><strong>WPKS - VERIFIKASI EMAIL</strong></span></h1>
                                            <p><strong>Selamat Datang</strong></p>
                                            <p><strong>untuk mengaktifkan akun anda Silhakan click&nbsp;</strong></p>
                                            <div style='text-align: center; margin : 50px'>
                                                <h1>
                                                    <a style='color:white ;border-radius: 5px; border: 1px rgb(0, 225, 255); background-color: #00ccff; padding: 20px 60px; text-decoration:none'
                                                    href='{baseUrl}/#!/account/confirmemail/{iduser}/{token}''>Disini</a>
                                                </h1>
                                            </div>
                                            <p>&nbsp;</p>";
        }

        public Task<User> UpdatePhotoProfile (int id, byte[] model) {

            var user = db.Users.Where (x => x.iduser == id).FirstOrDefault ();
            if (user != null && string.IsNullOrEmpty (user.photo)) {
                System.IO.File.Delete (Path.Combine (Directory.GetCurrentDirectory (), "wwwroot/images/avatar/") + user.photo);
            }
            var obj = Guid.NewGuid ();
            user.photo = obj.ToString () + ".png";
            System.IO.File.WriteAllBytes (Path.Combine (Directory.GetCurrentDirectory (), "wwwroot/images/avatar/") + user.photo, model);

            db.Users.Update (x => new { x.photo }, user, x => x.iduser == id);

            return Task.FromResult (user);

        }

        public Task<bool> changeStatusPembeli (int userId) {
            var transaction = db.BeginTransaction ();
            try {
                var pembeli = db.Pembeli.Where (x => x.iduser == userId).FirstOrDefault ();
                if (pembeli == null)
                    throw new SystemException ("Pembeli Tidak Ditemukan");
                pembeli.status = !pembeli.status;

                var updatePembeli = db.Pembeli.Update (x => new { x.status }, pembeli, x => x.idpembeli == pembeli.idpembeli);
                if (!updatePembeli) {
                    throw new SystemException ("Data Tidak Berhasil Diubah");
                }

                if (!db.Users.Update (x => new { x.status }, new User { status = pembeli.status }, x => x.iduser == userId))
                    throw new SystemException ("Data Tidak Berhasil Diubah");

                transaction.Commit ();
                return Task.FromResult (true);

            } catch (System.Exception ex) {
                transaction.Rollback ();
                throw new SystemException (ex.Message);
            }
        }

        public Task<bool> changeStatusPenjual (int userId) {
            var transaction = db.BeginTransaction ();
            try {
                var data = db.Penjual.Where (x => x.iduser == userId).FirstOrDefault ();
                if (data == null)
                    throw new SystemException ("Pembeli Tidak Ditemukan");
                data.status = !data.status;

                var updatePenjual = db.Penjual.Update (x => new { x.status }, data, x => x.idpenjual == data.idpenjual);
                if (!updatePenjual) {
                    throw new SystemException ("Data Tidak Berhasil Diubah");
                }

                if (!db.Users.Update (x => new { x.status }, new User { status = data.status }, x => x.iduser == userId))
                    throw new SystemException ("Data Tidak Berhasil Diubah");

                transaction.Commit ();
                return Task.FromResult (true);

            } catch (System.Exception ex) {
                transaction.Rollback ();
                throw new SystemException (ex.Message);
            }
        }
    }

    public static class UserExtention {
        public static User CreateUser (this User user, OcphDbContext db) {
            user.iduser = db.Users.InsertAndGetLastID (user);
            return user;
        }

        public static object profile (this User user, OcphDbContext db) {
            object data = null;
            if (user.role == "penjual") {
                var vdata = db.Penjual.Where (x => x.iduser == user.iduser).FirstOrDefault ();
                vdata.nama = vdata.nama_penjual;
                vdata.role = user.role;
                vdata.photo = vdata.foto_penjual;
                data = vdata;

            } else
            if (user.role == "pembeli") {
                var vdata = db.Pembeli.Where (x => x.iduser == user.iduser).FirstOrDefault ();
                vdata.nama = vdata.nama_pembeli;
                vdata.role = user.role;
                vdata.photo = vdata.foto_pembeli;
                data = vdata;
            } else {
                data = user;
            }
            return data;
        }

        public static User GetDataUser (this System.Security.Claims.ClaimsPrincipal user, OcphDbContext db) {
            var claim = user.Claims.Where (x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault ();
            var result = db.Users.Where (x => x.username == claim.Value).FirstOrDefault ();
            return result;
        }

        public static string GenerateToken (this User user, string secretCode) {
            var claims = new List<Claim> {
                new Claim (JwtRegisteredClaimNames.Sub, user.iduser.ToString ()),
                new Claim (JwtRegisteredClaimNames.Sub, user.email),
                new Claim (JwtRegisteredClaimNames.Sub, user.username),
                new Claim (JwtRegisteredClaimNames.Jti, Guid.NewGuid ().ToString ()),
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