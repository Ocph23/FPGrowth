using System;
 
 namespace MainWebApp.Models.Data  
{ 
     public interface IDataaset  
   {
         int iddataaset {  get; set;} 

         string nama_dataaset {  get; set;} 

         string tgl_transaksi {  get; set;} 

         string nama_pembeli {  get; set;} 

         string jenis_kayu {  get; set;} 

         string ukuran_kayu {  get; set;} 

         string jumlah_barang {  get; set;} 

         double harga {  get; set;} 

         string total_bayar {  get; set;} 

     }
}


