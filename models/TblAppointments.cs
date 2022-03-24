using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace SalonApp.Models
{
    public class TblAppointments
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required(ErrorMessage = "Please enter a valid time")]
        [DataType(DataType.Time)]
        public TimeSpan Time { get; set; }

        //[DataType(DataType.Date)]
        [Required(ErrorMessage = "Please enter a valid date")]
        public DateTime Date { get; set; }

        public string Treatment { get; set; }

        public int CustomerID { get; set; }

        [ForeignKey("CustomerID")]
        public TblCustomerReg TblCustomerReg { get; set; }



    }
}