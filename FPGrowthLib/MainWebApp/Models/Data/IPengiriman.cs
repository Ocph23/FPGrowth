using System;
 
 namespace MainWebApp.Models.Data 
{ 
     public interface IPengiriman  
   {
         int idpengiriman {  get; set;} 

         DateTime tgl_pengiriman {  get; set;} 

         int jumlah_barang {  get; set;} 

         string status_pengiriman {  get; set;} 

         string status_pengantaran {  get; set;} 

         string keterangan {  get; set;} 

         int idorder {  get; set;} 

     }
}


