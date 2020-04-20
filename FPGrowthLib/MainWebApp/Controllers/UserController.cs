using System;
using System.Linq;
using System.Net;
using System.Security;
using System.Security.Claims;
using System.Threading.Tasks;
using MainWebApp.Models;
using MainWebApp.Models.Data;
using MainWebApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace MainWebApp.Controllers {
    [ApiController]
    [Route ("[controller]/[action]")]
    public class UserController : ControllerBase {
        private IUserService _service;

        public UserController (IUserService service) {
            _service = service;
        }

        [HttpPost]
        public IActionResult login (UserLogin model) {
            try {
                return Ok (_service.Authenticate (model.UserName, model.Password));
            } catch (System.Exception ex) {
                return Unauthorized (ex.Message);
            }
        }

        [HttpGet]
        [Authorize]
        public IActionResult profile () {
            try {
                string userId = User.FindFirst (ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty (userId))
                    throw new System.Exception ("Anda Tidak Memiliki Akses");

                int Id = Convert.ToInt32 (userId);

                return Ok (_service.profile (Id));
            } catch (System.Exception ex) {
                return Unauthorized (ex.Message);
            }
        }

        [HttpGet]
        public IActionResult verifyemail (int userid, string token) {
            try {
                return Ok (_service.verifyemail (userid, token));
            } catch (System.Exception ex) {

                return Unauthorized (ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> RegisterPembeli (Pembeli model) {
            try {
                var data = await _service.RegisterPembeli (model);
                return Ok (data);
            } catch (System.Exception ex) {
                return BadRequest (ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> RegisterPenjual (Penjual model) {
            try {
                var data = await _service.RegisterPenjual (model);
                return Ok (data);
            } catch (System.Exception ex) {
                return BadRequest (ex.Message);
            }
        }

        [HttpPost]
        [Route ("photoprofile/{id}")]
        public async Task<IActionResult> photoprofile (int id, byte[] model) {
            try {
                return Ok (await _service.UpdatePhotoProfile (id, model););
            } catch (System.Exception ex) {
                return BadRequest (ex.Message);
            }
        }

    }
}