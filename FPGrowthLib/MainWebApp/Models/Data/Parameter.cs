using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ocph.DAL;

namespace MainWebApp.Models.Data {
     [TableName ("Parameter")]
     public class Parameter : IParameter {
          [PrimaryKey ("idnilai")]
          [DbColumn ("idnilai")]
          public int idnilai { get; set; }

          [DbColumn ("nilai_minimum_support")]
          public int nilai_minimum_support { get; set; }

          [DbColumn ("nilai_minimum_confidancce")]
          public double nilai_minimum_confidancce { get; set; }

          [DbColumn ("status")]
          public bool status { get; set; }

     }
}