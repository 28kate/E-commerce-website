using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace SalonApp.Models
{
    public class ShoppingCart
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int CustomerId { get; set; }  // [Foreign Key] // Virtual
        [ForeignKey("CustomerId")]           // Potential problem // Caps ID
        public TblCustomerReg TblCustomerReg { get; set; }

        [Display(Name = "Item Name")]
        public string ItemName { get; set; }

        public int Quantity { get; set; }

        public double Price { get; set; }

        [Display(Name = "Total Price")]
        public double QxPrice { get; set; }

        // Added Product Desc, Img:

        public string Desc { get; set; }
        public byte[] Image { get; set; }



        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual TblOrders TblOrders { get; set; }

    }
}

// Delete Cascade Code
//
// public void JustForShow()
// {
//    Products objP = DB.Products.Where(p => p.Id == IdFromView).Single(); // 1
//    ShoppingCart objS = DB.ShoppingCart.Where(s => s.ProductId = IdFromView);
//    DB.ShoppingCart.Remove(objS);
//    DB.Products.Remove(objP);
//    DB.SaveChanges();
//
// }
