using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ocph.DAL;

namespace MainWebApp.Models.Data {
     [TableName ("User")]
     public class User : IUser {
          [PrimaryKey ("iduser")]
          [DbColumn ("iduser")]
          public int iduser { get; set; }

          [DbColumn ("username")]
          public string username { get; set; }

          [DbColumn ("password")]
          public string password { get; set; }

          [DbColumn ("token")]
          public string token { get; set; }

          [DbColumn ("aktif")]
          public bool aktif { get; set; }

          [DbColumn ("created")]
          public DateTime created { get; set; }
          public List<string> roles { get; internal set; }
     }
}