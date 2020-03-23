using System;
 
 namespace MainWebApp.Models.Data  
{ 
     public interface IParameter  
   {
         int idnilai {  get; set;} 

         int nilai_minimum_support {  get; set;} 

         int nilai_minimum_confidancce {  get; set;} 

     }
}


