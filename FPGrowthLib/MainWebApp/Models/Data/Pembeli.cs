using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ocph.DAL;
 
 namespace MainWebApp.Models.Data 
{ 
     [TableName("Pembeli")] 
     public class Pembeli :IPembeli  
   {
          [PrimaryKey("idpembeli")] 
          [DbColumn("idpembeli")] 
          public int idpembeli {  get; set;} 

          [DbColumn("iduser")] 
          public int iduser {  get; set;} 

          [DbColumn("nama")] 
          public string nama {  get; set;} 

          [DbColumn("alamat")] 
          public string alamat {  get; set;} 

          [DbColumn("telepon")] 
          public string telepon {  get; set;} 

     }
}


