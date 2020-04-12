using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ocph.DAL;

namespace MainWebApp.Models.Data {
     [TableName ("Pembayaran")]
     public class Pembayaran : IPembayaran {
          [PrimaryKey ("idpembayaran")]
          [DbColumn ("idpembayaran")]
          public int idpembayaran { get; set; }

          [DbColumn ("tgl_pembayaran")]
          public DateTime tgl_pembayaran { get; set; }

          [DbColumn ("bank")]
          public string bank { get; set; }

          [DbColumn ("potongan")]
          public string potongan { get; set; }

          [DbColumn ("status_pembayaran")]
          public string status_pembayaran { get; set; }

          [DbColumn ("bukti_pembayaran")]
          public string bukti_pembayaran { get; set; }

          [DbColumn ("idorder")]
          public int idorder { get; set; }

          public byte[] data_bukti { get; set; }

     }
}