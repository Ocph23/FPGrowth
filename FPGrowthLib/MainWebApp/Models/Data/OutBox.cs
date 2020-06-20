using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ocph.DAL;

namespace MainWebApp.Models.Data {
     [TableName ("outbox")]
     public class OutBox {
          [PrimaryKey ("ID")]
          [DbColumn ("ID")]
          public int ID { get; set; }

          [DbColumn ("UDH")]
          public string UDH { get; set; }

          [DbColumn ("TextDecoded")]
          public string TextDecoded { get; set; }

          [DbColumn ("DestinationNumber")]
          public string DestinationNumber { get; set; }

          [DbColumn ("MultiPart")]
          public bool MultiPart { get; set; }

          [DbColumn ("CreatorID")]
          public string CreatorID { get; set; }

     }
}