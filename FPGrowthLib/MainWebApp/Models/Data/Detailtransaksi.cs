using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ocph.DAL;
 
 namespace MainWebApp.Models.Data 
{ 
     [TableName("Detailtransaksi")] 
     public class Detailtransaksi :IDetailtransaksi  
   {
          [PrimaryKey("iddetail")] 
          [DbColumn("iddetail")] 
          public int iddetail {  get; set;} 

          [DbColumn("idtransaksi")] 
          public int idtransaksi {  get; set;} 

          [DbColumn("jumlah")] 
          public int jumlah {  get; set;} 

          [DbColumn("harga")] 
          public double harga {  get; set;} 

          [DbColumn("idbarang")] 
          public int idbarang {  get; set;} 

     }
}


