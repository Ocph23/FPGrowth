using System;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace MainWebApp.Controllers {
    [ApiController]
    [Route ("api/[controller]")]
    public class BarangController : ControllerBase {
        private IOptions<AppSettings> _setting;

        public BarangController (IOptions<AppSettings> appSettings) {
            _setting = appSettings;
        }

        [HttpGet]
        public IActionResult Get () {
            try {

                using (var db = new OcphDbContext (_setting)) {
                    var result = from a in db.Barang.Select ()
                    join b in db.Kategori.Select () on a.idkategori equals b.idkategori
                    join c in db.Penjual.Select () on a.idpenjual equals c.idpenjual
                    select new Models.Data.Barang {
                    gambar = a.gambar, harga = a.harga, idbarang = a.idbarang,
                    idkategori = a.idkategori, idpenjual = a.idpenjual, keterangan = a.keterangan, lebar = a.lebar,
                    nama_barang = a.nama_barang, panjang = a.panjang, stock = a.stock, tgl_publish = a.tgl_publish, tinggi = a.tinggi, kategori = b,
                    penjual = c
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
                    var result = from a in db.Barang.Where (x => x.idbarang == id)
                    join b in db.Kategori.Select () on a.idkategori equals b.idkategori
                    join c in db.Penjual.Select () on a.idpenjual equals c.idpenjual
                    select new Models.Data.Barang {
                    gambar = a.gambar, harga = a.harga, idbarang = a.idbarang,
                    idkategori = a.idkategori, idpenjual = a.idpenjual, keterangan = a.keterangan, lebar = a.lebar,
                    nama_barang = a.nama_barang, panjang = a.panjang, stock = a.stock, tgl_publish = a.tgl_publish, tinggi = a.tinggi, kategori = b,
                    penjual = c
                    };
                    return Ok (result.FirstOrDefault ());
                }
            } catch (System.Exception ex) {
                return BadRequest (ex.Message);
            }
        }

        [HttpGet]
        [Route ("/bypenjualid/{id}")]
        public IActionResult Getbypenjualid (int id) {
            try {

                using (var db = new OcphDbContext (_setting)) {
                    var result = from a in db.Barang.Where (x => x.idpenjual == id)
                    join b in db.Kategori.Select () on a.idkategori equals b.idkategori
                    join c in db.Penjual.Select () on a.idpenjual equals c.idpenjual
                    select new Models.Data.Barang {
                    gambar = a.gambar, harga = a.harga, idbarang = a.idbarang,
                    idkategori = a.idkategori, idpenjual = a.idpenjual, keterangan = a.keterangan, lebar = a.lebar,
                    nama_barang = a.nama_barang, panjang = a.panjang, stock = a.stock, tgl_publish = a.tgl_publish, tinggi = a.tinggi, kategori = b,
                    penjual = c
                    };
                    return Ok (result.ToList ());
                }
            } catch (System.Exception ex) {
                return BadRequest (ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Post (Models.Data.Barang data) {
            try {
                using (var db = new OcphDbContext (_setting)) {
                    Guid obj = Guid.NewGuid ();
                    data.gambar = obj.ToString () + ".png";
                    var path = Path.Combine (
                        Directory.GetCurrentDirectory (), "wwwroot/images/barang",
                        data.gambar);

                    if (data.GambarData != null && data.GambarData.Length > 0) {

                        System.IO.File.WriteAllBytes (path, data.GambarData);
                    }

                    data.idbarang = db.Barang.InsertAndGetLastID (data);
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
        public IActionResult Put (Models.Data.Barang data) {
            try {
                if (data.GambarData != null) {

                    if (!string.IsNullOrEmpty (data.gambar)) {
                        var path1 = Path.Combine (
                            Directory.GetCurrentDirectory (), "wwwroot/images/barang",
                            data.gambar);

                        if (System.IO.File.Exists (path1)) {
                            System.IO.File.Delete (path1);
                        }
                    }

                    Guid obj = Guid.NewGuid ();
                    data.gambar = obj.ToString () + ".png";
                    var path = Path.Combine (
                        Directory.GetCurrentDirectory (), "wwwroot/images/barang",
                        data.gambar);
                    System.IO.File.WriteAllBytes (path, data.GambarData);
                }

                using (var db = new OcphDbContext (_setting)) {
                    var updated = db.Barang.Update (x => new { x.gambar, x.harga, x.idbarang, x.idkategori, x.keterangan, x.lebar, x.tinggi, x.panjang, x.stock }, data, x => x.idbarang == data.idbarang);
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
                    var deleted = db.Barang.Delete (x => x.idbarang == id);
                    if (!deleted) {
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