using System;
 
 namespace MainWebApp.Models.Data 
{ 
     public interface IOrder  
   {
         int idorder {  get; set;} 

         DateTime tgl_order {  get; set;} 

         DateTime wkt_exp_order {  get; set;} 

         int idpembeli {  get; set;} 

         int idmanajemen {  get; set;} 

         string alamatpengiriman {  get; set;} 

     }
}


