using Ocph.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainWebApp.Models
{
    [TableName("Role")]
    public class Role 
    {
        [PrimaryKey("idrole")]
        [DbColumn("idrole")]
        public int idrole { get; set; }

        [DbColumn("rolename")]
        public string rolename { get; set; }

    }
}
