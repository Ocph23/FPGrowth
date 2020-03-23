using System;
 
 namespace MainWebApp.Models.Data  
{ 
     public interface IPembayaran  
   {
         int idpembayaran {  get; set;} 

         DateTime tgl_pembayaran {  get; set;} 

         string nama_pembeli {  get; set;} 

         string no_tlp_pembeli {  get; set;} 

         string nama_bank_pembeli {  get; set;} 

         string nama_rek_pembeli {  get; set;} 

         string nama_toko {  get; set;} 

         string nama_barang {  get; set;} 

         int jumlah_barang {  get; set;} 

         string gambar {  get; set;} 

         double harga {  get; set;} 

         string nama_kategori {  get; set;} 

         string parent_kategori {  get; set;} 

         string alamat_pengiriman {  get; set;} 

         string potongan {  get; set;} 

         double total_bayar {  get; set;} 

         string status_pengantaran {  get; set;} 

         string status_pembayaran {  get; set;} 

         string bukti_pembayaran {  get; set;} 

         int idorder {  get; set;} 

     }
}


