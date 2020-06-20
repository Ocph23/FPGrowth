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

          [DbColumn ("role")]
          public string role { get; set; }

          [DbColumn ("email")]
          public string email { get; set; }

          [DbColumn ("no_tlp")]
          public string no_tlp { get; set; }

          [DbColumn ("kodeverifikasi")]
          public string kodeverifikasi { get; set; }

          [DbColumn ("photo")]
          public string photo { get; set; }

          [DbColumn ("status")]
          public bool status { get; set; }

          public string Token { get; set; }
     }
}