using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ocph.DAL;
 
 namespace MainWebApp.Models.Data  
{ 
     [TableName("Pengiriman")] 
     public class Pengiriman :IPengiriman  
   {
          [PrimaryKey("idpengiriman")] 
          [DbColumn("idpengiriman")] 
          public int idpengiriman {  get; set;} 

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
          public string total_bayar {  get; set;} 

          [DbColumn("nama_toko")] 
          public string nama_toko {  get; set;} 

          [DbColumn("potongan")] 
          public string potongan {  get; set;} 

          [DbColumn("bukti_pengiriman")] 
          public string bukti_pengiriman {  get; set;} 

          [DbColumn("verifikasi_pengiriman")] 
          public string verifikasi_pengiriman {  get; set;} 

          [DbColumn("status_pengiriman")] 
          public string status_pengiriman {  get; set;} 

          [DbColumn("status_pengantaran")] 
          public string status_pengantaran {  get; set;} 

          [DbColumn("keterangan")] 
          public string keterangan {  get; set;} 

          [DbColumn("idpembayaran")] 
          public int idpembayaran {  get; set;} 

     }
}


