using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ocph.DAL;
 
 namespace MainWebApp.Models.Data  
{ 
     [TableName("Dataaset")] 
     public class Dataaset :IDataaset  
   {
          [PrimaryKey("iddataaset")] 
          [DbColumn("iddataaset")] 
          public int iddataaset {  get; set;} 

          [DbColumn("nama_dataaset")] 
          public string nama_dataaset {  get; set;} 

          [DbColumn("tgl_transaksi")] 
          public string tgl_transaksi {  get; set;} 

          [DbColumn("nama_pembeli")] 
          public string nama_pembeli {  get; set;} 

          [DbColumn("jenis_kayu")] 
          public string jenis_kayu {  get; set;} 

          [DbColumn("ukuran_kayu")] 
          public string ukuran_kayu {  get; set;} 

          [DbColumn("jumlah_barang")] 
          public string jumlah_barang {  get; set;} 

          [DbColumn("harga")] 
          public double harga {  get; set;} 

          [DbColumn("total_bayar")] 
          public string total_bayar {  get; set;} 

     }
}


