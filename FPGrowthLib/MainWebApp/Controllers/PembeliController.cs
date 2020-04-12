using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace MainWebApp.Controllers {
    [ApiController]
    [Route ("api/[controller]")]
    public class PembeliController : ControllerBase {
        private IOptions<AppSettings> _setting;

        public PembeliController (IOptions<AppSettings> appSettings) {
            _setting = appSettings;
        }

        [HttpGet]
        public IActionResult Get () {
            try {

                using (var db = new OcphDbContext (_setting)) {
                    var result = db.Pembeli.Select ();
                    return Ok (result);
                }

            } catch (System.Exception ex) {
                return BadRequest (ex.Message);
            }
        }

        [HttpGet]
        [Route ("{id}")]
        public IActionResult GetById (int id) {
            try {
                using (var db = new OcphDbContext (_setting)) {
                    var result = db.Pembeli.Where (x => x.idpembeli == id).FirstOrDefault ();
                    return Ok (result);
                }
            } catch (System.Exception ex) {
                return BadRequest (ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Post (Models.Data.Pembeli data) {
            try {
                using (var db = new OcphDbContext (_setting)) {
                    data.idpembeli = db.Pembeli.InsertAndGetLastID (data);
                    if (data.idpembeli <= 0) {
                        throw new System.Exception ("Data tidak tersimpan");
                    }
                    return Ok (data);

                }
            } catch (System.Exception ex) {

                return BadRequest (ex.Message);
            }
        }

        [HttpPut]
        public IActionResult Put (Models.Data.Pembeli data) {
            try {
                using (var db = new OcphDbContext (_setting)) {
                    var updated = db.Pembeli.Update (x => new { x.alamat, x.email_pembeli, x.foto_ktp, x.jenis_kelamin, x.nama_pembeli, x.no_tlp, x.tgl_daftar, x.tgl_lahir },
                        data, x => x.idpembeli == data.idpembeli);
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
                    var deleted = db.Pembeli.Delete (x => x.idpembeli == id);
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