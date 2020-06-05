using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace MainWebApp.Controllers {
    [ApiController]
    [Route ("api/[controller]")]
    public class LaporanController : ControllerBase {
        private IOptions<AppSettings> _setting;

        public LaporanController (IOptions<AppSettings> appSettings) {
            _setting = appSettings;
        }

        [HttpGet]
        public IActionResult Get () {
            try {

                using (var db = new OcphDbContext (_setting)) {
                    var sql = @"SELECT
                                orders.idorder,
                                orders.tgl_order,
                                pembeli.nama_pembeli,
                                detailorder.jumlah,
                                detailorder.harga,
                                penjual.idpenjual,
                                penjual.nama_toko,
                                barang.idbarang,
                                barang.nama_barang,
                                pembayaran.tgl_pembayaran,
                                manajemen_transaksi.potongan,
                                pembayaran.status_pembayaran
                                FROM
                                detailorder
                                LEFT JOIN orders ON detailorder.idorder = orders.idorder
                                LEFT JOIN barang ON detailorder.idbarang = barang.idbarang
                                LEFT JOIN penjual ON barang.idpenjual = penjual.idpenjual
                                LEFT JOIN manajemen_transaksi ON orders.idmanajemen =
                                manajemen_transaksi.idmanajemen
                                LEFT JOIN pembeli ON orders.idpembeli = pembeli.idpembeli
                                LEFT JOIN pembayaran ON orders.idorder = pembayaran.idorder
                                where status_pembayaran='Lunas'";
                    var result = db.SelectDynamic (sql);

                    return Ok (result);
                }

            } catch (System.Exception ex) {
                return BadRequest (ex.Message);
            }
        }

    }
}