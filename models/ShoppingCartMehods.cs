using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalonApp.Models
{
    public class ShoppingCartMehods
    {
        private SalonContext db;
        public string AddToCart(int? productid, int? quantity, int? customerid)
        {
            db = new SalonContext();

            if (productid == null || quantity == null || customerid == null)
                return "null";
            ShoppingCart objShoppingCart = new ShoppingCart();


            if (db.ShoppingCart.Where(s => s.CustomerId == customerid.Value && s.ProductId == productid.Value).Any())
            {
                int QuanFromProducts = db.TblOrders.Where(p => p.Id == productid.Value).Select(p => p.Quantity).Single();
                objShoppingCart = db.ShoppingCart.Where(s => s.CustomerId == customerid.Value && s.ProductId == productid.Value).Single();
                if ((objShoppingCart.Quantity + quantity.Value) > QuanFromProducts)
                    return "Over Quantity";

                objShoppingCart.Quantity += quantity.Value;
                objShoppingCart.QxPrice = objShoppingCart.Quantity * objShoppingCart.Price;

            }
            else
            {

                TblOrders objOrders = db.TblOrders.Where(p => p.Id == productid.Value).SingleOrDefault();
                if (objOrders == null)
                    return "Product Not Found";
                objShoppingCart.CustomerId = customerid.Value;
                objShoppingCart.ItemName = objOrders.Name;
                objShoppingCart.Quantity = quantity.Value;
                objShoppingCart.Price = objOrders.Price;
                objShoppingCart.QxPrice = quantity.Value * objOrders.Price;
                objShoppingCart.ProductId = productid.Value;
                //Changes
                objShoppingCart.Desc = objOrders.Desc;
                objShoppingCart.Image = objOrders.Image;
                //
                db.ShoppingCart.Add(objShoppingCart);
            }
            db.SaveChanges();
            return "Successful";

        }

        public bool MinusFromCart(int? productid, int? customerid)
        {
            db = new SalonContext();

            if (productid == null || customerid == null)
                return false;

            ShoppingCart objShoppingCart = db.ShoppingCart.Where(s => s.CustomerId == customerid.Value && s.ProductId == productid.Value).SingleOrDefault();

            if ((objShoppingCart.Quantity - 1) < 1)
            {
                if (DeleteFromCart(productid.Value, customerid.Value) == false)
                    return false;
            }
            else
            {
                objShoppingCart.Quantity -= 1;
                objShoppingCart.QxPrice = objShoppingCart.Quantity * objShoppingCart.Price;
                db.SaveChanges();
            }
            return true;
        }

        public bool DeleteFromCart(int? productid, int? customerid)
        {
            db = new SalonContext();

            if (productid == null || customerid == null)
                return false;

            try
            {
                ShoppingCart objShoppingCart = db.ShoppingCart.Where(s => s.CustomerId == customerid.Value && s.ProductId == productid.Value).SingleOrDefault();
                db.ShoppingCart.Remove(objShoppingCart);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}