using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ocph.DAL;
 
 namespace MainWebApp.Models.Data 
{ 
     [TableName("Manajemen_Transaksi")] 
     public class Manajemen_Transaksi :IManajemen_Transaksi  
   {
          [PrimaryKey("idmanajemen")] 
          [DbColumn("idmanajemen")] 
          public int idmanajemen {  get; set;} 

          [DbColumn("nama_bank_pembayaran")] 
          public string nama_bank_pembayaran {  get; set;} 

          [DbColumn("no_rek_pembayaran")] 
          public string no_rek_pembayaran {  get; set;} 

          [DbColumn("bts_jumlah_pengiriman")] 
          public string bts_jumlah_pengiriman {  get; set;} 

          [DbColumn("potongan")] 
          public string potongan {  get; set;} 

          [DbColumn("status")] 
          public string status {  get; set;} 

     }
}


