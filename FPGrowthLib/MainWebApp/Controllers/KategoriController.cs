using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace MainWebApp.Controllers {
    [ApiController]
    [Route ("[controller]/[action]")]
    public class KategoriController : ControllerBase {
        private IOptions<AppSettings> _dbSerring;

        public KategoriController (IOptions<AppSettings> appSettings) {
            _dbSerring = appSettings;
        }

        [HttpGet]
        public IActionResult Get () {
            try {

                using (var db = new OcphDbContext (_dbSerring)) {
                    var result = db.Kategori.Select ();
                    return Ok (result);
                }

            } catch (System.Exception ex) {
                return BadRequest (ex.Message);
            }
        }

        [HttpGet]
        public IActionResult GetById (int id) {
            try {
                using (var db = new OcphDbContext (_dbSerring)) {
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
                using (var db = new OcphDbContext (_dbSerring)) {
                    data.idkategori = db.Kategori.InsertAndGetLastID (data);
                    if (data.idkategori <= 0) {
                        throw new System.Exception ("Data tidak tersimpan");
                    }
                    return Ok (data);

                }
            } catch (System.Exception) {

                throw;
            }
        }

        [HttpPut]
        public IActionResult Put (Models.Data.Kategori data) {
            try {
                using (var db = new OcphDbContext (_dbSerring)) {
                    var updated = db.Kategori.Update (x => new { x.idkategori }, data, x => x.idkategori == data.idkategori);
                    if (updated) {
                        throw new System.Exception ("Data tidak tersimpan");
                    }
                    return Ok (data);
                }
            } catch (System.Exception) {
                throw;
            }
        }
    }
}