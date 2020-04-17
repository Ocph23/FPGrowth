using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ocph.DAL;

namespace MainWebApp.Models.Data {
     [TableName ("Pesan")]
     public class Pesan : IPesan {
          [PrimaryKey ("idpesan")]
          [DbColumn ("idpesan")]
          public int idpesan { get; set; }

          [DbColumn ("tgl_pesan")]
          public DateTime tgl_pesan { get; set; }

          [DbColumn ("isi_pesan")]
          public string isi_pesan { get; set; }

          [DbColumn ("idpengirim")]
          public int idpengirim { get; set; }

          [DbColumn ("idpenerima")]
          public int idpenerima { get; set; }

          [DbColumn ("nama_pengirim")]
          public string pengirim { get; set; }
          public string avatar { get; set; }
     }
}