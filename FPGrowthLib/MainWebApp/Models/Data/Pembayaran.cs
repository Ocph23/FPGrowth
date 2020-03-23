using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ocph.DAL;
 
 namespace MainWebApp.Models.Data  
{ 
     [TableName("Pembayaran")] 
     public class Pembayaran :IPembayaran  
   {
          [PrimaryKey("idpembayaran")] 
          [DbColumn("idpembayaran")] 
          public int idpembayaran {  get; set;} 

          [DbColumn("tgl_pembayaran")] 
          public DateTime tgl_pembayaran {  get; set;} 

          [DbColumn("nama_pembeli")] 
          public string nama_pembeli {  get; set;} 

          [DbColumn("no_tlp_pembeli")] 
          public string no_tlp_pembeli {  get; set;} 

          [DbColumn("nama_bank_pembeli")] 
          public string nama_bank_pembeli {  get; set;} 

          [DbColumn("nama_rek_pembeli")] 
          public string nama_rek_pembeli {  get; set;} 

          [DbColumn("nama_toko")] 
          public string nama_toko {  get; set;} 

          [DbColumn("nama_barang")] 
          public string nama_barang {  get; set;} 

          [DbColumn("jumlah_barang")] 
          public int jumlah_barang {  get; set;} 

          [DbColumn("gambar")] 
          public string gambar {  get; set;} 

          [DbColumn("harga")] 
          public double harga {  get; set;} 

          [DbColumn("nama_kategori")] 
          public string nama_kategori {  get; set;} 

          [DbColumn("parent_kategori")] 
          public string parent_kategori {  get; set;} 

          [DbColumn("alamat_pengiriman")] 
          public string alamat_pengiriman {  get; set;} 

          [DbColumn("potongan")] 
          public string potongan {  get; set;} 

          [DbColumn("total_bayar")] 
          public double total_bayar {  get; set;} 

          [DbColumn("status_pengantaran")] 
          public string status_pengantaran {  get; set;} 

          [DbColumn("status_pembayaran")] 
          public string status_pembayaran {  get; set;} 

          [DbColumn("bukti_pembayaran")] 
          public string bukti_pembayaran {  get; set;} 

          [DbColumn("idorder")] 
          public int idorder {  get; set;} 

     }
}


