using System;
 
 namespace MainWebApp.Models.Data 
{ 
     public interface IPembayaran  
   {
         int idpembayaran {  get; set;} 

         DateTime tgl_pembayaran {  get; set;} 

         string bank {  get; set;} 

         string potongan {  get; set;} 

         string status_pembayaran {  get; set;} 

         string bukti_pembayaran {  get; set;} 

         int idorder {  get; set;} 

     }
}


