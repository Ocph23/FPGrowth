using System;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace MainWebApp.Controllers {
    [ApiController]
    [Route ("api/[controller]")]
    public class PenjualController : ControllerBase {
        private IOptions<AppSettings> _setting;

        public PenjualController (IOptions<AppSettings> appSettings) {
            _setting = appSettings;
        }

        [HttpGet]
        public IActionResult Get () {
            try {

                using (var db = new OcphDbContext (_setting)) {
                    var result = db.Penjual.Select ();
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
                    var result = db.Penjual.Where (x => x.idpenjual == id).FirstOrDefault ();
                    return Ok (result);
                }
            } catch (System.Exception ex) {
                return BadRequest (ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Post (Models.Data.Penjual data) {
            try {
                using (var db = new OcphDbContext (_setting)) {
                    data.idpenjual = db.Penjual.InsertAndGetLastID (data);
                    if (data.idpenjual <= 0) {
                        throw new System.Exception ("Data tidak tersimpan");
                    }
                    return Ok (data);

                }
            } catch (System.Exception ex) {

                return BadRequest (ex.Message);
            }
        }

        [HttpPut]
        public IActionResult Put (Models.Data.Penjual data) {
            using (var db = new OcphDbContext (_setting)) {
                var path = Path.Combine (
                    Directory.GetCurrentDirectory (), "wwwroot/images/penjual/");

                var trans = db.BeginTransaction ();
                try {
                    Guid obj = Guid.NewGuid ();
                    if (data.data_ktp != null && data.data_ktp.Length > 0) {
                        if (!string.IsNullOrEmpty (data.foto_ktp)) {
                            System.IO.File.Delete (path + data.foto_ktp);
                        }
                        data.foto_ktp = obj.ToString () + ".png";
                        System.IO.File.WriteAllBytes (path + data.foto_ktp, data.data_ktp);
                    }

                    obj = Guid.NewGuid ();
                    if (data.data_penjual != null && data.data_penjual.Length > 0) {
                        if (!string.IsNullOrEmpty (data.foto_penjual)) {
                            System.IO.File.Delete (path + data.foto_penjual);
                        }
                        data.foto_penjual = obj.ToString () + ".png";
                        System.IO.File.WriteAllBytes (path + data.foto_penjual, data.data_penjual);
                    }

                    obj = Guid.NewGuid ();
                    if (data.data_toko != null && data.data_toko.Length > 0) {
                        if (!string.IsNullOrEmpty (data.foto_toko)) {
                            System.IO.File.Delete (path + data.foto_toko);
                        }
                        data.foto_toko = obj.ToString () + ".png";
                        System.IO.File.WriteAllBytes (path + data.foto_toko, data.data_toko);
                    }

                    var updated = db.Penjual.Update (x => new { x.alamat, x.foto_ktp, x.foto_penjual, x.foto_toko, x.jenis_kelamin, x.nama_penjual, x.no_tlp, x.nama_toko, x.status },
                        data, x => x.idpenjual == data.idpenjual);
                    if (!updated) {
                        throw new System.Exception ("Data tidak tersimpan");
                    }
                    trans.Commit ();
                    return Ok (data);
                } catch (System.Exception ex) {
                    trans.Rollback ();
                    return BadRequest (ex.Message);
                }
            }
        }

        [HttpDelete]
        public IActionResult Delete (int id) {
            try {
                using (var db = new OcphDbContext (_setting)) {
                    var deleted = db.Penjual.Delete (x => x.idpenjual == id);
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