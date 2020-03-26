using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ocph.DAL;
 
 namespace MainWebApp.Models.Data 
{ 
     [TableName("Order")] 
     public class Order :IOrder  
   {
          [PrimaryKey("idorder")] 
          [DbColumn("idorder")] 
          public int idorder {  get; set;} 

          [DbColumn("tgl_order")] 
          public DateTime tgl_order {  get; set;} 

          [DbColumn("wkt_exp_order")] 
          public DateTime wkt_exp_order {  get; set;} 

          [DbColumn("idpembeli")] 
          public int idpembeli {  get; set;} 

          [DbColumn("idmanajemen")] 
          public int idmanajemen {  get; set;} 

          [DbColumn("alamatpengiriman")] 
          public string alamatpengiriman {  get; set;} 

     }
}


