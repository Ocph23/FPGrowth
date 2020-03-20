using Ocph.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainWebApp.Models
{
    [TableName("Userinrole")]
    public class UserInRole
    {
        [DbColumn("idrole")]
        public int idrole { get; set; }

        [DbColumn("iduser")]
        public int iduser { get; set; }

    }
}
