using System;

namespace MainWebApp.Models.Data {

     public interface IKomentar {
          int idkomentar { get; set; }

          DateTime tgl_komentar { get; set; }

          string isi_komentar { get; set; }

          int idbarang { get; set; }

          int iduser { get; set; }

     }
}