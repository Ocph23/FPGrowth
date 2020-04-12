using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ocph.DAL;

namespace MainWebApp.Models.Data {
     [TableName ("Komentar")]
     public class Komentar : IKomentar {

          [PrimaryKey ("idkomentar")]
          [DbColumn ("idkomentar")]
          public int idkomentar { get; set; }

          [DbColumn ("tgl_komentar")]
          public DateTime tgl_komentar { get; set; }

          [DbColumn ("isi_komentar")]
          public string isi_komentar { get; set; }

          [DbColumn ("idbarang")]
          public int idbarang { get; set; }

          [DbColumn ("iduser")]
          public int iduser { get; set; }

          [DbColumn ("nama")]
          public string nama { get; set; }
          public string email { get; set; }
          public string photo { get; set; }
     }
}