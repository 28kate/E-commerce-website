using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace SalonApp.Models
{
    public class Tblproduct
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProdID { get; set; }

        [Display(Name = "Name")]
        public string ProdName { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Price")]
        public double ProdPrice { get; set; }

        [Display(Name = "Description")]
        public string ProdDescrip { get; set; }

        [Display(Name = "Quantity")]
        public int ProdQuantity { get; set; }

    }
}