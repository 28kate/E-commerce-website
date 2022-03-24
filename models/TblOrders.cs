using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using System.Web;


namespace SalonApp.Models
{
    public class TblOrders
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter a product name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter a product price")]
        public double Price { get; set; }

        [Required(ErrorMessage = "Please enter a description")]
        [StringLength(50, ErrorMessage = "Please enter less than 50 characters")]
        [Display(Name = "Product Description")]
        public string Desc { get; set; }

        public int Quantity { get; set; }

        public byte[] Image { get; set; }

    }
}