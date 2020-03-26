using System;
 
 namespace MainWebApp.Models.Data 
{ 
     public interface IKategori  
   {
         int idkategori {  get; set;} 

         string kode_kategori {  get; set;} 

         string nama_kategori {  get; set;} 

         string deskripsi {  get; set;} 

     }
}


