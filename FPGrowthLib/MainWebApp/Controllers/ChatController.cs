using System;
using System.Linq;
using System.Security.Claims;
using MainWebApp.Models.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace MainWebApp.Controllers {
    [ApiController]
    [Route ("api/[controller]")]
    public class ChatController : ControllerBase {
        private IOptions<AppSettings> _setting;

        public ChatController (IOptions<AppSettings> appSettings) {
            _setting = appSettings;
        }

        [Authorize]
        [HttpGet]
        [Route ("chatwith/{id}")]
        public IActionResult chatwith (int id) {
            try {
                string userId = User.FindFirst (ClaimTypes.NameIdentifier)?.Value;
                int myId = Convert.ToInt32 (userId);
                using (var db = new OcphDbContext (_setting)) {
                    var result = from a in db.Chat.Where (x => (x.idpengirim == id && x.idpenerima == myId) || x.idpengirim == myId && x.idpenerima == id)
                    join b in db.Users.Select () on a.idpengirim equals b.iduser
                    select new Pesan { pengirim = a.pengirim, idpenerima = a.idpenerima, idpengirim = a.idpengirim, idpesan = a.idpesan, isi_pesan = a.isi_pesan, tgl_pesan = a.tgl_pesan, avatar = b.photo };
                    return Ok (result);
                }
            } catch (System.Exception ex) {
                return BadRequest (ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Post (Models.Data.Pesan data) {
            try {
                using (var db = new OcphDbContext (_setting)) {
                    data.idpesan = db.Chat.InsertAndGetLastID (data);
                    if (data.idpesan <= 0) {
                        throw new System.Exception ("Data tidak tersimpan");
                    }
                    return Ok (data);
                }
            } catch (System.Exception ex) {

                return BadRequest (ex.Message);
            }
        }

        [HttpDelete]
        public IActionResult Delete (int id) {
            try {
                using (var db = new OcphDbContext (_setting)) {
                    var deleted = db.Chat.Delete (x => x.idpesan == id);
                    if (deleted) {
                        throw new System.Exception ("Data tidak berhasil dihapus");
                    }
                    return Ok (true);
                }
            } catch (System.Exception) {
                throw;
            }
        }
    }
}