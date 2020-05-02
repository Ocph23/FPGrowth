using System;
using System.IO;
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
                    var result = from a in db.Pembeli.Select ()
                    join b in db.Users.Select () on a.iduser equals b.iduser
                    select new Models.Data.Pembeli {
                    alamat = a.alamat, email_pembeli = a.email_pembeli, foto_ktp = a.foto_ktp,
                    foto_pembeli = a.foto_pembeli, idpembeli = a.idpembeli, iduser = a.iduser,
                    jenis_kelamin = a.jenis_kelamin, nama_pembeli = a.nama_pembeli,
                    no_tlp = a.no_tlp, photo = b.photo, role = b.role, status = a.status,
                    tgl_daftar = a.tgl_daftar, tgl_lahir = a.tgl_lahir
                    };
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

            using (var db = new OcphDbContext (_setting)) {
                var path = Path.Combine (
                    Directory.GetCurrentDirectory (), "wwwroot/images/pembeli/");

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
                    if (data.data_pembeli != null && data.data_pembeli.Length > 0) {
                        if (!string.IsNullOrEmpty (data.foto_pembeli)) {
                            System.IO.File.Delete (Path.Combine (Directory.GetCurrentDirectory (), "wwwroot/images/avatar/") + data.foto_pembeli);
                        }
                        data.foto_pembeli = obj.ToString () + ".png";
                        System.IO.File.WriteAllBytes (Path.Combine (Directory.GetCurrentDirectory (), "wwwroot/images/avatar/") + data.foto_pembeli, data.data_pembeli);
                    }

                    var updated = db.Pembeli.Update (x => new { x.alamat, x.email_pembeli, x.foto_pembeli, x.foto_ktp, x.jenis_kelamin, x.nama_pembeli, x.no_tlp, x.tgl_daftar, x.tgl_lahir, x.status },
                        data, x => x.idpembeli == data.idpembeli);

                    db.Users.Update (x => new { x.photo }, new Models.Data.User { photo = data.foto_pembeli }, x => x.iduser == data.iduser);

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
        [Route ("{id}")]
        public IActionResult Delete (int id) {
            try {
                using (var db = new OcphDbContext (_setting)) {
                    var deleted = db.Pembeli.Delete (x => x.idpembeli == id);
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