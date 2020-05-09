using System;

namespace MainWebApp.Models.Data {

     public interface IPembeli {
          int idpembeli { get; set; }

          string nama_pembeli { get; set; }

          string email_pembeli { get; set; }

          string jenis_kelamin { get; set; }

          string alamat { get; set; }

          DateTime tgl_lahir { get; set; }

          string no_tlp { get; set; }

          string foto_pembeli { get; set; }

          string foto_ktp { get; set; }

          DateTime tgl_daftar { get; set; }

          bool status { get; set; }

          int iduser { get; set; }

          string nama { get; set; }

     }
}