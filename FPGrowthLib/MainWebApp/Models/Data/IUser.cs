using System;
 
 namespace MainWebApp.Models.Data 
{ 
     public interface IUser  
   {
         int iduser {  get; set;} 

         string username {  get; set;} 

         string password {  get; set;} 

         string token {  get; set;} 

         string aktif {  get; set;} 

         DateTime created {  get; set;} 

     }
}


