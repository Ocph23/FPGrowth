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

                    var algoritma = new Algoritma.AlgoritmaProccess (listData);

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

    }
}