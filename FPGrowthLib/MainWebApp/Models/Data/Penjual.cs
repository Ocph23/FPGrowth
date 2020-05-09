using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ocph.DAL;

namespace MainWebApp.Models.Data {
     [TableName ("Penjual")]
     public class Penjual : IPenjual {
          [PrimaryKey ("idpenjual")]
          [DbColumn ("idpenjual")]
          public int idpenjual { get; set; }

          [DbColumn ("nama_penjual")]
          public string nama_penjual { get; set; }

          [DbColumn ("no_tlp")]
          public string no_tlp { get; set; }

          [DbColumn ("jenis_kelamin")]
          public string jenis_kelamin { get; set; }

          [DbColumn ("alamat")]
          public string alamat { get; set; }

          [DbColumn ("tgl_lahir")]
          public DateTime tgl_lahir { get; set; }

          [DbColumn ("nama_toko")]
          public string nama_toko { get; set; }

          [DbColumn ("foto_ktp")]
          public string foto_ktp { get; set; }

          [DbColumn ("foto_penjual")]
          public string foto_penjual { get; set; }

          [DbColumn ("foto_toko")]
          public string foto_toko { get; set; }

          [DbColumn ("keterangan")]
          public string keterangan { get; set; }

          [DbColumn ("tgl_daftar")]
          public DateTime tgl_daftar { get; set; }

          [DbColumn ("status")]
          public bool status { get; set; }

          [DbColumn ("iduser")]
          public int iduser { get; set; }
          public string Password { get; set; }
          public string email { get; set; }

          public string nama { get; set; }

          public byte[] data_ktp { get; set; }

          public byte[] data_penjual { get; set; }

          public byte[] data_toko { get; set; }
          public string role { get; set; }
          public string photo { get; set; }
     }
}