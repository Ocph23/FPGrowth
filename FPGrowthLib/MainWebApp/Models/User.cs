using Ocph.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainWebApp.Models
{
    [TableName("User")]
    public class User 
    {
        [PrimaryKey("iduser")]
        [DbColumn("iduser")]
        public int iduser { get; set; }

        [DbColumn("username")]
        public string username { get; set; }

        [DbColumn("password")]
        public string password { get; set; }

        [DbColumn("token")]
        public string token { get; set; }

        [DbColumn("aktif")]
        public bool aktif { get; set; }

        [DbColumn("created")]
        public DateTime created { get; set; }
        public List<string> roles { get; set; }
    }
}
