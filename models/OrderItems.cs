using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalonApp.Models
{
    public class OrderItems
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

        public int Quantity { get; set; }

        public double Price { get; set; }

        [Display(Name = "Total Item Price")]
        public double QuanXPrice { get; set; }

        // Added Product Desc, Img:

        public string Desc { get; set; } // Take Out
        public byte[] Image { get; set; }

        [Required]
        public int OrderId { get; set; }

        [ForeignKey("OrderId")]
        public virtual OrderMain OrderMain { get; set; }
    }
}