using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ocph.DAL;

namespace MainWebApp.Models.Data {
     [TableName ("Orders")]
     public class Order : IOrder {
          [PrimaryKey ("idorder")]
          [DbColumn ("idorder")]
          public int idorder { get; set; }

          [DbColumn ("tgl_order")]
          public DateTime tgl_order { get; set; }

          [DbColumn ("wkt_exp_order")]
          public DateTime wkt_exp_order { get; set; }

          [DbColumn ("alamatpengiriman")]
          public string alamatpengiriman { get; set; }

          [DbColumn ("idpembeli")]
          public int idpembeli { get; set; }

          [DbColumn ("idmanajemen")]
          public int idmanajemen { get; set; }

          private bool expire;

          public string status {
               get {
                    var s = "Menunggu Pembayaran";
                    if (pembayaran != null)
                         s = pembayaran.status_pembayaran;
                    else if (Expire)
                         s = "Expire";
                    return s;
               }
          }

          public bool Expire {
               get {

                    var selisih = DateTime.Now.Subtract (wkt_exp_order);
                    if (selisih.TotalSeconds >= 0) {
                         expire = true;
                    } else {
                         expire = false;
                    }
                    return expire;
               }
          }

          public List<Transaksi> Data { get; set; }
          public Pembayaran pembayaran { get; set; }
          public Pengiriman pengiriman { get; set; }
          public Manajemen_Transaksi management { get; set; }
          public Pembeli pembeli { get; set; }
     }
}