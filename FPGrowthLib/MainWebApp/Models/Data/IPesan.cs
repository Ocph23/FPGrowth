using System;
 
 namespace MainWebApp.Models.Data  
{ 
     public interface IPesan  
   {
         int idpesan {  get; set;} 

         DateTime tgl_pesan {  get; set;} 

         string isi_pesan {  get; set;} 

         int idpembeli {  get; set;} 

     }
}


