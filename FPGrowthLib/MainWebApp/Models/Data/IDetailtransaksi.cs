using System;
 
 namespace MainWebApp.Models.Data 
{ 
     public interface IDetailtransaksi  
   {
         int iddetail {  get; set;} 

         int idtransaksi {  get; set;} 

         int jumlah {  get; set;} 

         double harga {  get; set;} 

         int idbarang {  get; set;} 

     }
}


