using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SalonApp.Models
{
    public class Appointments
    {


        [Required(ErrorMessage = "Please enter a valid time")]
        [DataType(DataType.Time)]
        public TimeSpan Time { get; set; }

        [Required(ErrorMessage = "Please enter a valid date")]
        //[DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required]
        public ListTreatment TreatmentList { get; set; }

        public enum ListTreatment
        {
            Haircuts__R15,
            Pedicures_R30,
            Waxing____R45,
            Threading_R25,
            Massages__R35
        }

    }
}