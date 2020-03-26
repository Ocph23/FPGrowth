using System;
 
 namespace MainWebApp.Models.Data 
{ 
     public interface ITransaksi  
   {
         int idtransaksi {  get; set;} 

         DateTime tgl_order {  get; set;} 

         int jumlah_barang {  get; set;} 

         double harga {  get; set;} 

         int idorder {  get; set;} 

         int idbarang {  get; set;} 

     }
}


