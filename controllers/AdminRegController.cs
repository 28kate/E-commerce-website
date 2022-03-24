using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SalonApp.Controllers
{
    public class AdminReg
    {


        [Required(ErrorMessage = "Please enter your first name")]
        [MinLength(3, ErrorMessage = "Please enter more than 3 characters")]
        [StringLength(30, ErrorMessage = "Please enter less than 30 characters")]
        [Display(Name = "Enter Your First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter your surname")]
        [MinLength(3, ErrorMessage = "Please enter more than 3 characters")]
        [StringLength(30, ErrorMessage = "Please enter less than 30 characters")]
        [Display(Name = "Enter Your Surname")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Please enter your email")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Enter Your Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter your phone number")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Please enter a username")]
        [MinLength(3, ErrorMessage = "Please enter more than 3 characters")]
        [StringLength(50, ErrorMessage = "Please enter less than 30 characters")]
        [Display(Name = "Enter a Username")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please enter a password")]
        [DataType(DataType.Password)]
        [Display(Name = "Enter a Password")]
        public string Password { get; set; }
    }
}