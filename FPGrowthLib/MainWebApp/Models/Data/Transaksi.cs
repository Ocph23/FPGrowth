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

          [DbColumn("jumlah_barang")] 
          public int jumlah_barang {  get; set;} 

          [DbColumn("harga")] 
          public double harga {  get; set;} 

          [DbColumn("idorder")] 
          public int idorder {  get; set;} 

          [DbColumn("idbarang")] 
          public int idbarang {  get; set;} 

     }
}


