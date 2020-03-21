using System;
 
 namespace MainWebApp.Models.Data 
{ 
     public interface IPembeli  
   {
         int idpembeli {  get; set;} 

         int iduser {  get; set;} 

         string nama {  get; set;} 

         string alamat {  get; set;} 

         string telepon {  get; set;} 

     }
}


