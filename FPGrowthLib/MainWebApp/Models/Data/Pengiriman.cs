using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ocph.DAL;

namespace MainWebApp.Models.Data {
     [TableName ("Pengiriman")]
     public class Pengiriman : IPengiriman {
          [PrimaryKey ("idpengiriman")]
          [DbColumn ("idpengiriman")]
          public int idpengiriman { get; set; }

          [DbColumn ("idorder")]
          public int idorder { get; set; }

          [DbColumn ("tgl_pengiriman")]
          public DateTime tgl_pengiriman { get; set; }

          [DbColumn ("jumlah_barang")]
          public int jumlah_barang { get; set; }

          [DbColumn ("status_pengiriman")]
          public string status_pengiriman { get; set; }

          [DbColumn ("status_pengantaran")]
          public string status_pengantaran { get; set; }

          [DbColumn ("keterangan")]
          public string keterangan { get; set; }

          [DbColumn ("bukti")]
          public string bukti { get; set; }
          public byte[] DataBukti { get; set; }

     }
}