using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SalonApp.Models
{
    public class TblDriverForLogin
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DriverLoginID { get; set; }

        public string Username { get; set; }


      
        [DataType(DataType.Password)]
        public string Password { get; set; }


    }
}