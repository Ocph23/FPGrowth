using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FPGrowthLib;
using MainWebApp.Models.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace MainWebApp.Controllers {
    [ApiController]
    [Route ("api/[controller]")]
    public class RekomendasiController : ControllerBase {
        private IOptions<AppSettings> _setting;

        public RekomendasiController (IOptions<AppSettings> appSettings) {
            _setting = appSettings;
        }

        [HttpGet]
        [Route ("{id}")]
        public IActionResult GetById (int id) {
            using (var db = new OcphDbContext (_setting)) {
                try {
                var orders = from a in db.Transaksi.Select ()
                join d in db.Barang.Select () on a.idbarang equals d.idbarang
                join k in db.Kategori.Select () on d.idkategori equals k.idkategori
                select new Transaksi {
                idorder = a.idorder,
                iddetailorder = a.iddetailorder,
                idbarang = a.idbarang,
                KodeKategori = k.kode_kategori
                    };

                    var listData = new List<DataItem> ();
                    var index = 1;
                    foreach (var items in orders.GroupBy (x => x.idorder)) {
                        var dataItem = new DataItem { TID = index, Items = new List<string> () };
                        foreach (var data in items.GroupBy (x => x.KodeKategori)) {
                            dataItem.Items.Add (data.Key);
                        }
                        listData.Add (dataItem);

                        index++;
                    }

                    var algoritma = new Algoritma.AlgoritmaProccess (15, 0.3, getData ());

                    return Ok (new {
                        Source = algoritma.Source, Frekuensi = algoritma.Frekuensi,
                            FrekuensiItemSet = algoritma.FrekuensiItemSet, ListItemSet = algoritma.ListItemSet,
                            ResultX = algoritma.ResultX, Vertices = algoritma.Vertices, Graphs = algoritma.Graphs,
                            ListConditionFPTree = algoritma.ListConditionFPTree, ItemSortPriority = algoritma.ItemSortPriority, ListItemSetResult = algoritma.ListItemSetResult,
                    });
                } catch (System.Exception ex) {
                    return BadRequest (ex.Message);
                }
            }
        }

        [HttpPost]
        [Route ("byalgoritma")]
        public IActionResult byalgoritma (AlgoParam param) {
            using (var db = new OcphDbContext (_setting)) {
                try {
                var orders = from a in db.Transaksi.Select ()
                join d in db.Barang.Select () on a.idbarang equals d.idbarang
                join k in db.Kategori.Select () on d.idkategori equals k.idkategori
                select new Transaksi {
                idorder = a.idorder,
                iddetailorder = a.iddetailorder,
                idbarang = a.idbarang,
                KodeKategori = k.kode_kategori
                    };

                    var listData = new List<DataItem> ();
                    var index = 1;
                    foreach (var items in orders.GroupBy (x => x.idorder)) {
                        var dataItem = new DataItem { TID = index, Items = new List<string> () };
                        foreach (var data in items.GroupBy (x => x.KodeKategori)) {
                            dataItem.Items.Add (data.Key);
                        }
                        listData.Add (dataItem);

                        index++;
                    }

                    var algoritma = new Algoritma.AlgoritmaProccess (param.MinSupport, param.Confidance, getData ());

                    return Ok (new {
                        Source = algoritma.Source, Frekuensi = algoritma.Frekuensi,
                            FrekuensiItemSet = algoritma.FrekuensiItemSet, ListItemSet = algoritma.ListItemSet,
                            ResultX = algoritma.ResultX, Vertices = algoritma.Vertices, Graphs = algoritma.Graphs,
                            ListConditionFPTree = algoritma.ListConditionFPTree, ItemSortPriority = algoritma.ItemSortPriority, ListItemSetResult = algoritma.ListItemSetResult,
                    });
                } catch (System.Exception ex) {
                    return BadRequest (ex.Message);
                }
            }
        }

        [Route ("byidbarang/{id}")]
        public IActionResult ByPenjualId (int id) {
            using (var db = new OcphDbContext (_setting)) {
                try {
                    var barangSearch = (from b in db.Barang.Where (x => x.idbarang == id) join k in db.Kategori.Select () on b.idkategori equals k.idkategori select new Barang { idpenjual = b.idpenjual, idbarang = b.idbarang, panjang = b.panjang, lebar = b.lebar, tinggi = b.tinggi, kategori = k, idkategori = k.idkategori }).FirstOrDefault ();
                    var orders = from a in db.Transaksi.Select ()
                    join o in db.Order.Select () on a.idorder equals o.idorder
                    join p in db.Pembeli.Select () on o.idpembeli equals p.idpembeli
                    join d in db.Barang.Where (x => x.idpenjual == barangSearch.idpenjual) on a.idbarang equals d.idbarang
                    join k in db.Kategori.Select () on d.idkategori equals k.idkategori
                    select new TransaksiBarang {
                        idorder = a.idorder,
                        iddetailorder = a.iddetailorder,
                        barang = d,
                        idbarang = a.idbarang, NamaPembeli = p.nama_pembeli,
                        KodeKategori = k.kode_kategori, TanggalOrder = o.tgl_order, harga = d.harga, jumlah = a.jumlah
                    };

                    var ukurans = orders.GroupBy (x => x.Ukuran);
                    List<Ukuran> Ukurans = new List<Ukuran> ();
                    var index = 1;
                    foreach (var item in ukurans) {
                        var b = orders.Where (x => x.Ukuran == item.Key).FirstOrDefault ();
                        if (b != null)
                            Ukurans.Add (new Ukuran (index, b.barang));
                        index++;
                    }

                    var listData = new List<DataItem> ();
                    index = 1;
                    foreach (var items in orders.GroupBy (x => x.idorder)) {
                        var dataItem = new DataItem { TID = index, Items = new List<string> () };
                        foreach (var data in items) {
                            var uk = Ukurans.Where (x => x.Name == data.Ukuran).FirstOrDefault ();
                            var jenis = $"{data.KodeKategori}-{uk.Id}";
                            dataItem.Items.Add (jenis);
                        }
                        listData.Add (dataItem);

                        index++;
                    }

                    var algoritma = new Algoritma.AlgoritmaProccess (15, 0.3, listData);

                    var brg = new TransaksiBarang { idbarang = id };

                    var recomendations = new List<Recomendation> ();

                    foreach (var item in algoritma.ListItemSetResult) {
                        var rek = GetRekomendation (item);
                        recomendations.Add (rek);
                    }

                    var searchRecomendation = recomendations.Where (x => x.KategoriRow == barangSearch.kategori.kode_kategori).ToList ();

                    var resss = from k in db.Kategori.Select ()
                    join b in db.Barang.Select () on k.idkategori equals b.idkategori
                    join a in searchRecomendation on k.kode_kategori equals a.KategoriColumn
                    join u in Ukurans on new { Ukuran = a.UkuraColumn, panjang = b.panjang, lebar = b.lebar, tinggi = b.tinggi }
                    equals new { Ukuran = u.Id, panjang = u.Panjang, lebar = u.Lebar, tinggi = u.Tinggi }
                    select new Barang {
                    nama_barang = b.nama_barang, tgl_publish = b.tgl_publish,
                    gambar = b.gambar, stock = b.stock, harga = b.harga, keterangan = b.keterangan,
                    idpenjual = b.idpenjual, idbarang = b.idbarang, panjang = b.panjang, lebar = b.lebar, tinggi = b.tinggi,
                    kategori = k, idkategori = k.idkategori
                    };

                    return Ok (resss.Where (x => x.idbarang != barangSearch.idbarang && x.idpenjual == barangSearch.idpenjual)
                        .GroupBy (x => x.idbarang, (key, g) => g.OrderBy (e => e.idbarang).First ()));
                } catch (System.Exception ex) {
                    return BadRequest (ex.Message);
                }
            }
        }

        private Recomendation GetRekomendation (ItemSet item) {

            var rows = item.Row.Split ('-');
            var row = Tuple.Create (rows[0], Convert.ToInt32 (rows[1]));
            var columns = item.Column.Split ('-');
            var column = Tuple.Create (columns[0], Convert.ToInt32 (columns[1]));
            return new Recomendation { KategoriRow = rows[0], KategoriColumn = columns[0], UkuraColumn = Convert.ToInt32 (columns[1]), UkuranRow = Convert.ToInt32 (rows[1]) };
        }

        private List<DataItem> getData () {
            var datas = new List<DataItem> ();
            datas.Add (new DataItem { TID = 1, Items = new List<string> { "M1", "L2" } });
            datas.Add (new DataItem { TID = 2, Items = new List<string> { "L2", "M1", "L3" } });
            datas.Add (new DataItem { TID = 3, Items = new List<string> { "M3", "L2" } });
            datas.Add (new DataItem { TID = 4, Items = new List<string> { "L2" } });
            datas.Add (new DataItem { TID = 5, Items = new List<string> { "B4", "B6", "M3", "L2" } });
            datas.Add (new DataItem { TID = 6, Items = new List<string> { "Li3" } });
            datas.Add (new DataItem { TID = 7, Items = new List<string> { "B6" } });
            datas.Add (new DataItem { TID = 8, Items = new List<string> { "L2", "M1" } });
            datas.Add (new DataItem { TID = 9, Items = new List<string> { "B4" } });
            datas.Add (new DataItem { TID = 10, Items = new List<string> { "B2", "M3" } });
            datas.Add (new DataItem { TID = 11, Items = new List<string> { "M1" } });
            datas.Add (new DataItem { TID = 12, Items = new List<string> { "M1", "L2", "L3", "B3", "B2", "B4" } });
            datas.Add (new DataItem { TID = 13, Items = new List<string> { "M3" } });
            datas.Add (new DataItem { TID = 14, Items = new List<string> { "B2", "L2", "B6", "L3" } });
            datas.Add (new DataItem { TID = 15, Items = new List<string> { "L3", "B4" } });
            datas.Add (new DataItem { TID = 16, Items = new List<string> { "B4", "B3" } });
            datas.Add (new DataItem { TID = 17, Items = new List<string> { "M1", "M3", "B4" } });
            datas.Add (new DataItem { TID = 18, Items = new List<string> { "B4", "L2", "B6", "M3" } });
            datas.Add (new DataItem { TID = 19, Items = new List<string> { "M1", "L2", "M7" } });
            datas.Add (new DataItem { TID = 20, Items = new List<string> { "M1", "L2", "M7" } });
            datas.Add (new DataItem { TID = 21, Items = new List<string> { "B4", "B2", "B3" } });
            datas.Add (new DataItem { TID = 22, Items = new List<string> { "M1", "L2", "M7" } });
            return datas;
        }

    }

    public class AlgoParam {
        public int IdPenjual { get; set; }
        public double MinSupport { get; set; }
        public double Confidance { get; set; }
    }

    public class Recomendation {
        public string KategoriRow { get; set; }
        public string KategoriColumn { get; set; }
        public int UkuranRow { get; set; }
        public int UkuraColumn { get; set; }

    }

    public class TransaksiBarang : IDetailOrder {
        public Barang barang;
        public int iddetailorder { get; set; }
        public int jumlah { get; set; }
        public double harga { get; set; }
        public int idorder { get; set; }
        public int idbarang { get; set; }

        public string KodeBarang {
            get {
                return $"BRG{idbarang:D3}";
            }
        }

        public string KodeKategori { get; set; }
        public DateTime TanggalOrder { get; internal set; }
        public string NamaPembeli { get; internal set; }
        public string Ukuran { get { return $"{barang.panjang} x {barang.lebar} x {barang.tinggi}"; } }
    }

}

public class Ukuran {

    public Ukuran (int id, Barang barang) {
        Id = id;
        Panjang = barang.panjang;
        Lebar = barang.lebar;
        Tinggi = barang.tinggi;
    }

    public int Id { get; set; }
    public string Name { get { return $"{Panjang} x {Lebar} x {Tinggi}"; } }
    public double Panjang { get; }
    public double Lebar { get; }
    public double Tinggi { get; }
}