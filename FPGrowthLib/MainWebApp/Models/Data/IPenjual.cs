using System;
 
 namespace MainWebApp.Models.Data 
{ 
     public interface IPenjual  
   {
         int idpenjual {  get; set;} 

         string nama {  get; set;} 

         string alamat {  get; set;} 

         string telepon {  get; set;} 

         int iduser {  get; set;} 

     }
}


