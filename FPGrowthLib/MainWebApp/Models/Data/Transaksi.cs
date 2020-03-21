using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ocph.DAL;
 
 namespace MainWebApp.Models.Data 
{ 
     [TableName("Transaksi")] 
     public class Transaksi :ITransaksi  
   {
          [PrimaryKey("idtransaksi")] 
          [DbColumn("idtransaksi")] 
          public int idtransaksi {  get; set;} 

          [DbColumn("idpembeli")] 
          public int idpembeli {  get; set;} 

          [DbColumn("tanggal")] 
          public DateTime tanggal {  get; set;} 

     }
}


