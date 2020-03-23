using System;
 
 namespace MainWebApp.Models.Data  
{ 
     public interface IManajemen_Transaksi  
   {
         int idmanajemen {  get; set;} 

         string nama_bank_pembayaran {  get; set;} 

         string no_rek_pembayaran {  get; set;} 

         string bts_jumlah_pengiriman {  get; set;} 

         string potongan {  get; set;} 

         string status {  get; set;} 

     }
}


