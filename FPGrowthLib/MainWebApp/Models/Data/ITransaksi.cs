using System;
 
 namespace MainWebApp.Models.Data  
{ 
     public interface ITransaksi  
   {
         int idtransaksi {  get; set;} 

         DateTime tgl_order {  get; set;} 

         DateTime tgl_pembayaran {  get; set;} 

         DateTime tgl_pengiriman {  get; set;} 

         string nama_pembeli {  get; set;} 

         string nama_barang {  get; set;} 

         string nama_kategori {  get; set;} 

         string parent_kategori {  get; set;} 

         int jumlah_barang {  get; set;} 

         double harga {  get; set;} 

         string alamat_pengiriman {  get; set;} 

         double total_bayar {  get; set;} 

         string nama_toko {  get; set;} 

         string potongan {  get; set;} 

         string bukti_pembayaran {  get; set;} 

         string status_pembayaran {  get; set;} 

         string status_pengiriman {  get; set;} 

         string status_transaksi {  get; set;} 

         int idpengiriman {  get; set;} 

     }
}


