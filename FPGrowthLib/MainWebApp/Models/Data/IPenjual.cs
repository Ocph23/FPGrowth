using System;

namespace MainWebApp.Models.Data {

     public interface IPenjual {
          int idpenjual { get; set; }

          string nama_penjual { get; set; }

          string no_tlp { get; set; }

          string jenis_kelamin { get; set; }

          string alamat { get; set; }

          DateTime tgl_lahir { get; set; }

          string nama_toko { get; set; }

          string foto_ktp { get; set; }

          string foto_penjual { get; set; }

          string foto_toko { get; set; }

          string keterangan { get; set; }

          bool status { get; set; }

          int iduser { get; set; }

          string nama { get; set; }

     }
}