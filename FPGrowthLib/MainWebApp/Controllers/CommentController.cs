using System.Linq;
using MainWebApp.Models.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace MainWebApp.Controllers {
    [ApiController]
    [Route ("api/[controller]")]

    public class CommentController : ControllerBase {
        private IOptions<AppSettings> _setting;

        public CommentController (IOptions<AppSettings> appSettings) {
            _setting = appSettings;
        }

        [HttpGet]
        [Route ("{id}")]
        public IActionResult GetById (int id) {
            try {
                using (var db = new OcphDbContext (_setting)) {
                    var result = from a in db.Komentar.Where (x => x.idbarang == id)
                    join b in db.Users.Select () on a.iduser equals b.iduser
                    select new Komentar () {
                    photo = b.photo, email = b.email, idbarang = a.idbarang, idkomentar = a.idkomentar,
                    iduser = a.iduser, isi_komentar = a.isi_komentar, tgl_komentar = a.tgl_komentar, nama = a.nama
                    };
                    return Ok (result);
                }
            } catch (System.Exception ex) {
                return BadRequest (ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Post (Models.Data.Komentar data) {
            try {
                using (var db = new OcphDbContext (_setting)) {
                    data.idbarang = db.Komentar.InsertAndGetLastID (data);
                    if (data.idbarang <= 0) {
                        throw new System.Exception ("Data tidak tersimpan");
                    }
                    return Ok (data);

                }
            } catch (System.Exception ex) {

                return BadRequest (ex.Message);
            }
        }

        [HttpPut]
        public IActionResult Put (Models.Data.Komentar data) {
            try {
                using (var db = new OcphDbContext (_setting)) {
                    var updated = db.Komentar.Update (x => new { x.isi_komentar }, data, x => x.idbarang == data.idbarang);
                    if (!updated) {
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
                    var deleted = db.Komentar.Delete (x => x.idbarang == id);
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