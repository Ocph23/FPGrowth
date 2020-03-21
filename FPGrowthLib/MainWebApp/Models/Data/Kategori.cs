using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ocph.DAL;
 
 namespace MainWebApp.Models.Data 
{ 
     [TableName("Kategori")] 
     public class Kategori :IKategori  
   {
          [PrimaryKey("idkategori")] 
          [DbColumn("idkategori")] 
          public int idkategori {  get; set;} 

     }
}


