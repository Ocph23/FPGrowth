using System;
using System.IO;
using System.Linq;
using System.Security.Claims;
using MainWebApp.Models.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace MainWebApp.Controllers {
    [ApiController]
    [Route ("api/[controller]")]
    public class PembelianController : ControllerBase {
        private IOptions<AppSettings> _setting;

        public PembelianController (IOptions<AppSettings> appSettings) {
            _setting = appSettings;
        }

        [HttpGet]
        public IActionResult Get () {
            try {
                using (var db = new OcphDbContext (_setting)) {
                    try {
                    var orders = from a in db.Order.Select ()
                    join m in db.Pembeli.Select () on a.idpembeli equals m.idpembeli into mm
                    from m in mm.DefaultIfEmpty ()
                    join p in db.Pembayaran.Select () on a.idorder equals p.idorder into pp
                    from p in pp.DefaultIfEmpty ()
                    join b in db.Transaksi.Select () on a.idorder equals b.idorder into gg
                    from b in gg.DefaultIfEmpty ()
                    select new Order {
                    idmanajemen = a.idmanajemen, alamatpengiriman = a.alamatpengiriman,
                    idorder = a.idorder, idpembeli = a.idpembeli, tgl_order = a.tgl_order,
                    wkt_exp_order = a.wkt_exp_order, pembayaran = p, Data = gg.ToList (), pembeli = m
                        };

                        return Ok (orders);
                    } catch (System.Exception ex) {
                        return BadRequest (ex.Message);
                    }
                }

            } catch (System.Exception ex) {
                return BadRequest (ex.Message);
            }
        }

        [HttpGet]
        [Route ("GetOrderByIdPembeli/{idpembeli}")]
        public IActionResult GetOrderByIdPembeli (int idpembeli) {
            using (var db = new OcphDbContext (_setting)) {
                try {
                var orders = from a in db.Order.Where (x => x.idpembeli == idpembeli)
                join p in db.Pembayaran.Select () on a.idorder equals p.idorder into pp
                from p in pp.DefaultIfEmpty ()
                join b in db.Transaksi.Select () on a.idorder equals b.idorder into gg
                from b in gg.DefaultIfEmpty ()
                select new Order {
                idmanajemen = a.idmanajemen, alamatpengiriman = a.alamatpengiriman,
                idorder = a.idorder, idpembeli = a.idpembeli, tgl_order = a.tgl_order,
                wkt_exp_order = a.wkt_exp_order, pembayaran = p, Data = gg.ToList ()
                    };

                    return Ok (orders);
                } catch (System.Exception ex) {
                    return BadRequest (ex.Message);
                }
            }
        }

        [HttpPost]
        [Route ("CreateOrder")]
        public IActionResult CreateOrder (Models.Data.Order order) {
            using (var db = new OcphDbContext (_setting)) {
                var transaction = db.BeginTransaction ();
                try {
                    order.tgl_order = DateTime.Now;
                    order.wkt_exp_order = order.tgl_order.AddHours (5);
                    order.idorder = db.Order.InsertAndGetLastID (order);
                    if (order.idorder <= 0)
                        throw new System.Exception ("Order Gagal Dibuat");

                    foreach (var item in order.Data) {
                        var barang = db.Barang.Where (x => x.idbarang == item.idbarang).FirstOrDefault ();
                        if (barang != null && (barang.stock - item.jumlah) >= 0) {
                            item.idorder = order.idorder;
                            item.iddetailorder = db.Transaksi.InsertAndGetLastID (item);
                            if (item.iddetailorder <= 0)
                                throw new System.Exception ("Order Gagal Dibuat");

                            if (barang != null) {

                            }
                            barang.stock -= item.jumlah;
                            db.Barang.Update (x => new { x.stock }, barang, x => x.idbarang == item.idbarang);
                        }
                    }
                    transaction.Commit ();
                    return Ok (order);
                } catch (System.Exception ex) {
                    transaction.Rollback ();
                    return BadRequest (ex.Message);
                }
            }
        }

        [HttpPost]
        [Route ("CreatePembayaran")]
        public IActionResult CreatePembayaran (Models.Data.Pembayaran model) {
            using (var db = new OcphDbContext (_setting)) {
                var transaction = db.BeginTransaction ();
                try {

                    if (model.data_bukti != null) {
                        Guid obj = Guid.NewGuid ();
                        model.bukti_pembayaran = obj.ToString () + ".png";
                        var path = Path.Combine (
                            Directory.GetCurrentDirectory (), "wwwroot/images/bukti",
                            model.bukti_pembayaran);

                        System.IO.File.WriteAllBytes (path, model.data_bukti);
                    }

                    model.idpembayaran = db.Pembayaran.InsertAndGetLastID (model);
                    if (model.idpembayaran <= 0)
                        throw new System.Exception ("Pembayaran Gagal Dibuat");
                    transaction.Commit ();
                    return Ok (model);
                } catch (System.Exception ex) {
                    transaction.Rollback ();
                    return BadRequest (ex.Message);
                }
            }
        }

        [HttpGet]
        [Route ("verifikasiPembayaran/{idpembayaran}")]
        public IActionResult verifikasiPembayaran (int idpembayaran) {
            using (var db = new OcphDbContext (_setting)) {
                try {

                    var updated = db.Pembayaran.Update (x => new { x.status_pembayaran }, new Pembayaran { status_pembayaran = "Lunas" }, x => x.idpembayaran == idpembayaran);
                    if (updated)
                        return Ok (updated);
                    else throw new SystemException ("Verfikasi Pembayaran Gagal");
                } catch (System.Exception ex) {
                    return BadRequest (ex.Message);
                }
            }
        }

        [HttpPost]
        [Route ("CreatePengiriman")]
        public IActionResult CreatePengiriman (Models.Data.Pengiriman model) {
            using (var db = new OcphDbContext (_setting)) {
                var transaction = db.BeginTransaction ();
                try {

                    if (model.idpengiriman <= 0) {
                        if (model.data_bukti_pengiriman != null) {
                            Guid obj = Guid.NewGuid ();
                            model.bukti_pengiriman = obj.ToString () + ".png";
                            var path = Path.Combine (
                                Directory.GetCurrentDirectory (), "wwwroot/images/bukti",
                                model.bukti_pengiriman);
                            System.IO.File.WriteAllBytes (path, model.data_bukti_pengiriman);
                        }

                        model.status_pengantaran = "Sudah";

                        model.idpengiriman = db.Pengiriman.InsertAndGetLastID (model);
                        if (model.idpengiriman <= 0)
                            throw new System.Exception ("Pengiriman Gagal Dibuat");
                    } else {
                        if (model.data_bukti_pengiriman != null) {
                            string path = "";

                            if (!string.IsNullOrEmpty (model.bukti_pengiriman)) {
                                path = Path.Combine (
                                    Directory.GetCurrentDirectory (), "wwwroot/images/bukti",
                                    model.bukti_pengiriman);
                                System.IO.File.Delete (path);
                            }

                            Guid obj = Guid.NewGuid ();
                            model.bukti_pengiriman = obj.ToString () + ".png";
                            path = Path.Combine (
                                Directory.GetCurrentDirectory (), "wwwroot/images/bukti",
                                model.bukti_pengiriman);

                            System.IO.File.WriteAllBytes (path, model.data_bukti_pengiriman);
                        }
                        model.status_pengantaran = "Sudah";
                        var updated = db.Pengiriman.Update (x => new {
                            x.bukti_pengiriman, x.jumlah_barang, x.keterangan, x.status_pengantaran, x.tgl_pengiriman
                        }, model, x => x.idpengiriman == model.idpengiriman);

                        if (!updated)
                            throw new System.Exception ("Pengiriman Gagal Dibuat");
                    }

                    transaction.Commit ();
                    return Ok (model);
                } catch (System.Exception ex) {
                    transaction.Rollback ();
                    return BadRequest (ex.Message);
                }
            }
        }

        [HttpGet]
        [Route ("GetOrderByPenjualId/{id}")]
        public IActionResult GetOrderByPenjualId (int id) {
            try {
                string sql = string.Format (@"SELECT
                            orders.idorder,
                            orders.tgl_order,
                            orders.wkt_exp_order,
                            orders.idpembeli,
                            orders.idmanajemen,
                            pembayaran.idpembayaran,
                            pembayaran.tgl_pembayaran,
                            pembeli.nama_pembeli,
                            pembeli.no_tlp,
                            pembeli.alamat,
                            pengiriman.status_pengantaran,
                            pengiriman.idpengiriman,
                            pengiriman.tgl_pengiriman,
                            pengiriman.jumlah_barang,
                            pengiriman.status_pengantaran,
                            pengiriman.keterangan,
                            pengiriman.idorder,
                            pengiriman.bukti_pengiriman,
                            orders.alamatpengiriman,
                            detailorder.idbarang,
                            detailorder.harga,
                            detailorder.jumlah,
                            barang.nama_barang,
                            barang.idpenjual,
                            barang.tgl_publish,
                            pembayaran.status_pembayaran,
                            manajemen_transaksi.potongan,
                            manajemen_transaksi.bts_jumlah_pengiriman,
                            pembayaran.bukti_pembayaran
                            FROM
                            orders
                            LEFT JOIN pembayaran ON orders.idorder = pembayaran.idorder
                            LEFT JOIN pengiriman ON orders.idorder = pengiriman.idorder
                            LEFT JOIN pembeli ON orders.idpembeli = pembeli.idpembeli
                            LEFT JOIN detailorder ON orders.idorder = detailorder.idorder
                            LEFT JOIN barang ON detailorder.idbarang = barang.idbarang
                            LEFT JOIN manajemen_transaksi ON orders.idmanajemen =
                            manajemen_transaksi.idmanajemen
                            where idpenjual={0}", id);
                using (var db = new OcphDbContext (_setting)) {
                    var result = db.SelectDynamic (sql);
                    return Ok (result);
                }
            } catch (System.Exception ex) {
                return BadRequest (ex.Message);
            }
        }

    }
}