using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace SalonApp.Models
{
    public class TblCustomerReg
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int CustomerID { get; set; }

        public string FirstName { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }


        public bool IsAdmin { get; set; }

        [Required(ErrorMessage = "Please enter a username")]
        [MinLength(3, ErrorMessage = "Please enter more than 3 characters")]
        [StringLength(50, ErrorMessage = "Please enter less than 30 characters")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please enter a password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string ResetPasswordCode { get; set; }

        public static implicit operator TblCustomerReg(bool v) => throw new NotImplementedException();

       
    }
}