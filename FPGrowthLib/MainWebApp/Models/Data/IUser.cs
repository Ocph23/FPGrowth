using System;

namespace MainWebApp.Models.Data {

     public interface IUser {
          int iduser { get; set; }

          string username { get; set; }

          string password { get; set; }

          string role { get; set; }

          string email { get; set; }

          string no_tlp { get; set; }

          string kodeverifikasi { get; set; }
          bool status { get; set; }

     }
}