using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace MainWebApp.Controllers {
    [ApiController]
    [Route ("api/[controller]")]
    public class ManagementTransaksiController : ControllerBase {
        private IOptions<AppSettings> _setting;

        public ManagementTransaksiController (IOptions<AppSettings> appSettings) {
            _setting = appSettings;
        }

        [HttpGet]
        public IActionResult Get () {
            try {

                using (var db = new OcphDbContext (_setting)) {
                    var result = db.ManagementTransaksi.Select ();
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
                    var result = db.ManagementTransaksi.Where (x => x.idmanajemen == id).FirstOrDefault ();
                    return Ok (result);
                }
            } catch (System.Exception ex) {
                return BadRequest (ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Post (Models.Data.Manajemen_Transaksi data) {
            try {
                using (var db = new OcphDbContext (_setting)) {
                    data.idmanajemen = db.ManagementTransaksi.InsertAndGetLastID (data);
                    if (data.idmanajemen <= 0) {
                        throw new System.Exception ("Data tidak tersimpan");
                    }
                    return Ok (data);

                }
            } catch (System.Exception ex) {

                return BadRequest (ex.Message);
            }
        }

        [HttpPut]
        public IActionResult Put (Models.Data.Manajemen_Transaksi data) {
            try {
                using (var db = new OcphDbContext (_setting)) {
                    var updated = db.ManagementTransaksi.Update (x => new { x.bts_jumlah_pengiriman, x.nama_bank_pembayaran, x.no_rek_pembayaran, x.potongan }, data, x => x.idmanajemen == data.idmanajemen);
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
                    var deleted = db.ManagementTransaksi.Delete (x => x.idmanajemen == id);
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