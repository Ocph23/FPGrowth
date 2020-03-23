using System;
 
 namespace MainWebApp.Models.Data  
{ 
     public interface IPengiriman  
   {
         int idpengiriman {  get; set;} 

         DateTime tgl_pengiriman {  get; set;} 

         string nama_pembeli {  get; set;} 

         string nama_barang {  get; set;} 

         string nama_kategori {  get; set;} 

         string parent_kategori {  get; set;} 

         int jumlah_barang {  get; set;} 

         double harga {  get; set;} 

         string alamat_pengiriman {  get; set;} 

         string total_bayar {  get; set;} 

         string nama_toko {  get; set;} 

         string potongan {  get; set;} 

         string bukti_pengiriman {  get; set;} 

         string verifikasi_pengiriman {  get; set;} 

         string status_pengiriman {  get; set;} 

         string status_pengantaran {  get; set;} 

         string keterangan {  get; set;} 

         int idpembayaran {  get; set;} 

     }
}


