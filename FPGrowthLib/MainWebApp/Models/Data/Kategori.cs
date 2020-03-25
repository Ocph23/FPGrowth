using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ocph.DAL;

namespace MainWebApp.Models.Data {
     [TableName ("Kategori")]
     public class Kategori : IKategori {
          [PrimaryKey ("idkategori")]
          [DbColumn ("idkategori")]
          public int idkategori { get; set; }

          [DbColumn ("nama_kategori")]
          public string nama_kategori { get; set; }

          [DbColumn ("deskripsi")]
          public string deskripsi { get; set; }

          [DbColumn ("kode_kategori")]
          public string kode_kategori { get; set; }

     }
}