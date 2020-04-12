using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ocph.DAL;

namespace MainWebApp.Models.Data {
     [TableName ("Pembeli")]
     public class Pembeli : IPembeli {
          [PrimaryKey ("idpembeli")]
          [DbColumn ("idpembeli")]
          public int idpembeli { get; set; }

          [DbColumn ("nama_pembeli")]
          public string nama_pembeli { get; set; }

          [DbColumn ("email_pembeli")]
          public string email_pembeli { get; set; }

          [DbColumn ("jenis_kelamin")]
          public string jenis_kelamin { get; set; }

          [DbColumn ("alamat")]
          public string alamat { get; set; }

          [DbColumn ("tgl_lahir")]
          public DateTime tgl_lahir { get; set; }

          [DbColumn ("no_tlp")]
          public string no_tlp { get; set; }

          [DbColumn ("foto_pembeli")]
          public string foto_pembeli { get; set; }

          [DbColumn ("foto_ktp")]
          public string foto_ktp { get; set; }

          [DbColumn ("tgl_daftar")]
          public DateTime tgl_daftar { get; set; }

          [DbColumn ("status")]
          public string status { get; set; }

          [DbColumn ("iduser")]
          public int iduser { get; set; }

          public string Password { get; set; }
          public string nama { get; set; }
          public string role { get; set; }
          public string photo { get; set; }
     }
}