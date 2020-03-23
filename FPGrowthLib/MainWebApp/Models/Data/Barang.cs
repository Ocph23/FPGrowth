using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ocph.DAL;
 
 namespace MainWebApp.Models.Data  
{ 
     [TableName("Barang")] 
     public class Barang :IBarang  
   {
          [PrimaryKey("idbarang")] 
          [DbColumn("idbarang")] 
          public int idbarang {  get; set;} 

          [DbColumn("tgl_publish")] 
          public DateTime tgl_publish {  get; set;} 

          [DbColumn("nama_barang")] 
          public string nama_barang {  get; set;} 

          [DbColumn("gambar")] 
          public string gambar {  get; set;} 

          [DbColumn("stock")] 
          public string stock {  get; set;} 

          [DbColumn("harga")] 
          public double harga {  get; set;} 

          [DbColumn("keterangan")] 
          public string keterangan {  get; set;} 

          [DbColumn("panjang")] 
          public string panjang {  get; set;} 

          [DbColumn("lebar")] 
          public string lebar {  get; set;} 

          [DbColumn("tinggi")] 
          public string tinggi {  get; set;} 

          [DbColumn("idkategori")] 
          public int idkategori {  get; set;} 

          [DbColumn("idpenjual")] 
          public int idpenjual {  get; set;} 

     }
}


