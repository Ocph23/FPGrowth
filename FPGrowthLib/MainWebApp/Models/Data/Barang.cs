using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ocph.DAL;
 
 namespace MainWebApp.Models.Data 
{ 
     [TableName("Barang")] 
     public class Barang :IBarang  
   {
          [PrimaryKey("idbarang")] 
          [DbColumn("idbarang")] 
          public int idbarang {  get; set;} 

          [DbColumn("kode")] 
          public string kode {  get; set;} 

          [DbColumn("nama")] 
          public string nama {  get; set;} 

     }
}


