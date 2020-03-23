using System.Net;
using System.Security;
using MainWebApp.Models;
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

        public IActionResult verifyemail (int userid, string token) {
            try {
                return Ok (_service.verifyemail (userid, token));
            } catch (System.Exception ex) {

                return Unauthorized (ex.Message);
            }
        }

    }
}