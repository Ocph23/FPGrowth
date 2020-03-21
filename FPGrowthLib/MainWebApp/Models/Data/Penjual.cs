using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ocph.DAL;
 
 namespace MainWebApp.Models.Data 
{ 
     [TableName("Penjual")] 
     public class Penjual :IPenjual  
   {
          [PrimaryKey("idpenjual")] 
          [DbColumn("idpenjual")] 
          public int idpenjual {  get; set;} 

          [DbColumn("nama")] 
          public string nama {  get; set;} 

          [DbColumn("alamat")] 
          public string alamat {  get; set;} 

          [DbColumn("telepon")] 
          public string telepon {  get; set;} 

          [DbColumn("iduser")] 
          public int iduser {  get; set;} 

     }
}


