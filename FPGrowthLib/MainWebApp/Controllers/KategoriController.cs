using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace MainWebApp.Controllers {
    [ApiController]
    [Route ("api/[controller]")]
    public class KategoriController : ControllerBase {
        private IOptions<AppSettings> _setting;

        public KategoriController (IOptions<AppSettings> appSettings) {
            _setting = appSettings;
        }

        [HttpGet]
        public IActionResult Get () {
            try {

                using (var db = new OcphDbContext (_setting)) {
                    var result = db.Kategori.Select ();
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
                    var result = db.Kategori.Where (x => x.idkategori == id).FirstOrDefault ();
                    return Ok (result);
                }
            } catch (System.Exception ex) {
                return BadRequest (ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Post (Models.Data.Kategori data) {
            try {
                using (var db = new OcphDbContext (_setting)) {
                    data.idkategori = db.Kategori.InsertAndGetLastID (data);
                    if (data.idkategori <= 0) {
                        throw new System.Exception ("Data tidak tersimpan");
                    }
                    return Ok (data);

                }
            } catch (System.Exception ex) {

                return BadRequest (ex.Message);
            }
        }

        [HttpPut]
        public IActionResult Put (Models.Data.Kategori data) {
            try {
                using (var db = new OcphDbContext (_setting)) {
                    var updated = db.Kategori.Update (x => new { x.kode_kategori, x.nama_kategori, x.deskripsi }, data, x => x.idkategori == data.idkategori);
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
                    var deleted = db.Kategori.Delete (x => x.idkategori == id);
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