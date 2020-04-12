using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ocph.DAL;

namespace MainWebApp.Models.Data {
     [TableName ("DetailOrder")]
     public class Transaksi : IDetailOrder {
          [PrimaryKey ("iddetailorder")]
          [DbColumn ("iddetailorder")]
          public int iddetailorder { get; set; }

          [DbColumn ("jumlah")]
          public int jumlah { get; set; }

          [DbColumn ("harga")]
          public double harga { get; set; }

          [DbColumn ("idorder")]
          public int idorder { get; set; }

          [DbColumn ("idbarang")]
          public int idbarang { get; set; }
     }
}