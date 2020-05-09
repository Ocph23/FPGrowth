using System;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace MainWebApp.Controllers {
    [ApiController]
    [Route ("api/[controller]")]
    public class ParameterController : ControllerBase {
        private IOptions<AppSettings> _setting;

        public ParameterController (IOptions<AppSettings> appSettings) {
            _setting = appSettings;
        }

        [HttpGet]
        public IActionResult Get () {
            try {

                using (var db = new OcphDbContext (_setting)) {
                    var result = db.Parameters.Select ();
                    return Ok (result.ToList ());
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
                    var result = db.Parameters.Where (x => x.idnilai == id).FirstOrDefault ();
                    return Ok (result);
                }
            } catch (System.Exception ex) {
                return BadRequest (ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Post (Models.Data.Parameter data) {
            try {
                using (var db = new OcphDbContext (_setting)) {
                    data.idnilai = db.Parameters.InsertAndGetLastID (data);
                    if (data.idnilai <= 0) {
                        throw new System.Exception ("Data tidak tersimpan");
                    }
                    return Ok (data);

                }
            } catch (System.Exception ex) {

                return BadRequest (ex.Message);
            }
        }

        [HttpPut]
        public IActionResult Put (Models.Data.Parameter data) {

            try {
                using (var db = new OcphDbContext (_setting)) {

                    var updated = db.Parameters.Update (x => new { x.status, x.nilai_minimum_confidancce, x.nilai_minimum_support }, data, x => x.idnilai == data.idnilai);

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
        [Route ("{id}")]
        public IActionResult Delete (int id) {
            try {
                using (var db = new OcphDbContext (_setting)) {
                    var deleted = db.Parameters.Delete (x => x.idnilai == id);
                    if (deleted) {
                        throw new System.Exception ("Data tidak berhasil dihapus");
                    }
                    return Ok (true);
                }
            } catch (System.Exception ex) {
                return BadRequest (ex.Message);
            }
        }
    }
}