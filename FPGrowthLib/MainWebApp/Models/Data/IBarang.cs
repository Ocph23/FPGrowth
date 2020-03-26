using System;
 
 namespace MainWebApp.Models.Data 
{ 
     public interface IBarang  
   {
         int idbarang {  get; set;} 

         DateTime tgl_publish {  get; set;} 

         string nama_barang {  get; set;} 

         string gambar {  get; set;} 

         string stock {  get; set;} 

         double harga {  get; set;} 

         string keterangan {  get; set;} 

         string panjang {  get; set;} 

         string lebar {  get; set;} 

         string tinggi {  get; set;} 

         int idkategori {  get; set;} 

         int idpenjual {  get; set;} 

     }
}


