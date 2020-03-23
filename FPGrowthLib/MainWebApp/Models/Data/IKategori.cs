using System;
 
 namespace MainWebApp.Models.Data  
{ 
     public interface IKategori  
   {
         int idkategori {  get; set;} 

         string nama_kategori {  get; set;} 

         string kategori_parent {  get; set;} 

         string kode_kategori {  get; set;} 

     }
}


