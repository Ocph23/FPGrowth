using System;
 
 namespace MainWebApp.Models.Data 
{ 
     public interface ITransaksi  
   {
         int idtransaksi {  get; set;} 

         int idpembeli {  get; set;} 

         DateTime tanggal {  get; set;} 

     }
}


