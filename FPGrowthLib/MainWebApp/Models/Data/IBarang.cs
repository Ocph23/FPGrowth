using System;

namespace MainWebApp.Models.Data {

     public interface IBarang {
          int idbarang { get; set; }

          DateTime tgl_publish { get; set; }

          string nama_barang { get; set; }

          string gambar { get; set; }

          int stock { get; set; }

          double harga { get; set; }

          string keterangan { get; set; }

          double panjang { get; set; }

          double lebar { get; set; }

          double tinggi { get; set; }

          int idkategori { get; set; }

          int idpenjual { get; set; }

          byte[] GambarData { get; set; }

          Kategori kategori { get; set; }
          Penjual penjual { get; set; }

     }
}