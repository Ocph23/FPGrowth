using System;

namespace MainWebApp.Models.Data {

     public interface IDetailOrder {
          int iddetailorder { get; set; }

          int jumlah { get; set; }

          double harga { get; set; }

          int idorder { get; set; }

          int idbarang { get; set; }

     }
}