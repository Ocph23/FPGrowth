using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ocph.DAL;
 
 namespace MainWebApp.Models.Data  
{ 
     [TableName("Transaksi")] 
     public class Transaksi :ITransaksi  
   {
          [PrimaryKey("idtransaksi")] 
          [DbColumn("idtransaksi")] 
          public int idtransaksi {  get; set;} 

          [DbColumn("tgl_order")] 
          public DateTime tgl_order {  get; set;} 

          [DbColumn("tgl_pembayaran")] 
          public DateTime tgl_pembayaran {  get; set;} 

          [DbColumn("tgl_pengiriman")] 
          public DateTime tgl_pengiriman {  get; set;} 

          [DbColumn("nama_pembeli")] 
          public string nama_pembeli {  get; set;} 

          [DbColumn("nama_barang")] 
          public string nama_barang {  get; set;} 

          [DbColumn("nama_kategori")] 
          public string nama_kategori {  get; set;} 

          [DbColumn("parent_kategori")] 
          public string parent_kategori {  get; set;} 

          [DbColumn("jumlah_barang")] 
          public int jumlah_barang {  get; set;} 

          [DbColumn("harga")] 
          public double harga {  get; set;} 

          [DbColumn("alamat_pengiriman")] 
          public string alamat_pengiriman {  get; set;} 

          [DbColumn("total_bayar")] 
          public double total_bayar {  get; set;} 

          [DbColumn("nama_toko")] 
          public string nama_toko {  get; set;} 

          [DbColumn("potongan")] 
          public string potongan {  get; set;} 

          [DbColumn("bukti_pembayaran")] 
          public string bukti_pembayaran {  get; set;} 

          [DbColumn("status_pembayaran")] 
          public string status_pembayaran {  get; set;} 

          [DbColumn("status_pengiriman")] 
          public string status_pengiriman {  get; set;} 

          [DbColumn("status_transaksi")] 
          public string status_transaksi {  get; set;} 

          [DbColumn("idpengiriman")] 
          public int idpengiriman {  get; set;} 

     }
}


