using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SalonApp.Models;
using System.Net.Mail;
using System.Threading.Tasks;

namespace SalonApp.Controllers
{
    public class CartController : Controller
    {
        private SalonContext db;
        // GET: Cart
        public ActionResult Index()
        {
            db = new SalonContext();
            List<TblOrders> objP = db.TblOrders.ToList();
            return View(objP);
        }

        public ActionResult Orders()
        {
            db = new SalonContext();
            int CusId = int.Parse(Session["ID"].ToString());
            var OrderObj = db.OrderItems.Include("OrderMain").Where(o => o.OrderMain.CustomerId == CusId && o.OrderId == o.OrderMain.Id).ToList();
            return View("Orders", OrderObj);
        }

        [HttpPost]
        public ActionResult SearchOrder(DateTime? datepicker)
        {
            if (datepicker == null)
                return RedirectToAction("Orders");
            db = new SalonContext();
            int CusId = int.Parse(Session["ID"].ToString());
            var OrderObj = db.OrderItems.Include("OrderMain").Where(o => o.OrderMain.CustomerId == CusId && o.OrderId == o.OrderMain.Id && DbFunctions.TruncateTime(o.OrderMain.DateOrdered) == DbFunctions.TruncateTime(datepicker.Value)).ToList();
            return View("Orders", OrderObj);
        }

        public ActionResult CartView()
        {
            Session["model"] = null;
            db = new SalonContext();
            int CusId = int.Parse(Session["ID"].ToString());
            IEnumerable<ShoppingCart> objS = db.ShoppingCart.Include("TblOrders").Where(s => s.CustomerId == CusId).ToList();
            return View(objS);

        }

        //public ActionResult Purchases()
        //{
        //    Session["purchConf"] = null;
        //    ViewBag.PurchSConf = null;
        //    return View();
        //}

        [HttpPost]
        public ActionResult Checkout()
        {
            db = new SalonContext();
            double total = 0;
            int CusId = int.Parse(Session["ID"].ToString());
            if (Session["model"] != null)
            {
                IEnumerable<ShoppingCart> model = (IEnumerable<ShoppingCart>)Session["model"];
                Session["purchConf"] = "You have succesfully purchased!";
                if (model.Count() > 0)
                {
                    OrderMain objMain = new OrderMain
                    {
                        CustomerId = CusId,
                        DateOrdered = DateTime.Now.Date,    
                        TimeOrdered = DateTime.Now.TimeOfDay,
                        FinalPrice = model.Sum(s => s.QxPrice)

                   
                    };
                    //objMain.Status = ;
                    total = objMain.FinalPrice;
                    db.OrderMain.Add(objMain);
                    foreach (var item in model)
                    {
                        OrderItems objItems = new OrderItems
                        {
                            OrderMain = objMain,
                            Name = item.ItemName,
                            Quantity = item.Quantity,
                            Price = item.Price,
                            QuanXPrice = item.QxPrice,
                            Desc = item.Desc,
                            Image = item.Image
                        };
                        db.OrderItems.Add(objItems);


                        TblOrders objOrder = db.TblOrders.Find(item.ProductId);
                        objOrder.Quantity -= item.Quantity;

                        db.ShoppingCart.Remove(db.ShoppingCart.Where(x => x.Id == item.Id).First());
                    }
                    db.SaveChanges();

                    //EmailStart

                    //using (var context = new SalonContext())
                    //{
                    //    var getCart = (from s in context.TblCustomerRegs where s.CustomerID == CusId select s).FirstOrDefault();
                    //    if (getCart != null)
                    //    {

                    //        var subject = "Ralene's Beauty Salon: " +
                    //                      "Your Order Details";
                    //        var body = "Hi " + getCart.FirstName + ",\n" +
                    //                      "Thank you for choosing Ralene's salon for your beauty needs. \n" +
                    //                      "You recently made a purchase on our site. \n\n" +
                    //                      "Your purchase is as follows: \n";
                    //        foreach (var item in model)
                    //        {
                    //            body += "Item: " + item.ItemName + "\n" +
                    //        "Quantity: " + item.Quantity + "\n" +
                    //        "Total: " + item.QxPrice + "\n\n";
                    //        }
                    //        body += "You can also check your order history via our website. \n\n" +
                    //           "Contact Us: \n" +
                    //           "Phone: 082 648 7848 \n\n" +
                    //           "Address: \n" +
                    //           "602 Mount Batten Drive \n" +
                    //           "Resevoir Hills \n" +
                    //           "Durban \n\n" +
                    //           "The Alpha Unit \n" + DateTime.Now + "\n\n";

                    //        //  SendEmail(CartEmail, body, subject);
                    //        var SalonEm = new MailAddress("TrialApp69@gmail.com", "Ralenes Beauty Salon");
                    //        var email = new MailAddress(getCart.Email);
                    //        var pass = "Trial6921";
                    //        //var Sub = subject;
                    //        //var Bod = body;
                    //        var smtp = new SmtpClient
                    //        {
                    //            Host = "smtp.gmail.com",
                    //            Port = 587,
                    //            EnableSsl = true,
                    //            DeliveryMethod = SmtpDeliveryMethod.Network,
                    //            UseDefaultCredentials = false,
                    //            Credentials = new NetworkCredential(SalonEm.Address, pass)
                    //        };
                    //        using (var message = new MailMessage(SalonEm, email)
                    //        {
                    //            Subject = subject,
                    //            Body = body
                    //        })
                    //        {
                    //            smtp.Send(message);
                    //        }
                    //    }
                    //}
                    //EmailEnd

                    // Yall can change the following
                    // var OrderObj = db.OrderItems.Include("OrderMain").Where(o => o.OrderMain.CustomerId == CusId && o.OrderId == o.OrderMain.Id).ToList();
                    //return View("Orders", OrderObj);

                   // return RedirectToAction("Payment", new { total });

                    return RedirectToAction("ShippingInfo", objMain);
                }
            }
            return RedirectToAction("CartView");
            //return RedirectToAction("CartView","Cart");

        }

        public ActionResult ShippingInfo(OrderMain objMain)
        {
            return View();
        }

        [ActionName("ShippingInfo")]
        [HttpPost]
        public ActionResult ShippingInfoPost(OrderMain objMain)
        {
            db = new SalonContext();
            if(string.IsNullOrEmpty(objMain.DeliveryAddress) || string.IsNullOrEmpty(objMain.DeliverySuburb) || string.IsNullOrEmpty(objMain.DeliveryZipCode))
            {
                ModelState.AddModelError(string.Empty, "Please make sure all fields are filled in");
                return View(objMain);
            }
            objMain.FinalPrice = objMain.CalcFinalTotal();
            db.SaveChanges();

            //EmailStart

            using (var context = new SalonContext())
            {
                if (objMain != null)
                {
                    var customerDetails = db.TblCustomerRegs.Find(objMain.CustomerId);
                    var model = db.OrderItems.Where(o => o.OrderId == objMain.Id);

                    var subject = "Ralene's Beauty Salon: " +
                                  "Your Order Details";
                    var body = "Hi " + customerDetails.FirstName + ",\n" +
                                  "Thank you for choosing Ralene's salon for your beauty needs. \n" +
                                  "You recently made a purchase on our site. \n\n" +
                                  "Your purchase is as follows: \n";
                    foreach (var item in model)
                    {
                        body += "Item: " + item.Name + "\n" +
                    "Quantity: " + item.Quantity + "\n" +
                    "Total: " + item.QuanXPrice.ToString("C") + "\n\n";
                    }
                    string DeliveryType = objMain.Delivery.ToString();
                    if (DeliveryType.Contains("_"))
                        DeliveryType = DeliveryType.Replace("_", " ");
                    body += "Delivery Type: " + DeliveryType + "\n";
                    if (objMain.Delivery == OrderMain.DeliveryType.Standard_Delivery || objMain.Delivery == OrderMain.DeliveryType.Express_Delivery)
                        body += "Delivery Cost: " + objMain.GetDeliveryPrice(objMain.Delivery).ToString("C")+"\n";

                    body += "\nFinal Price: " + objMain.FinalPrice.ToString("C")+"\n\n";

                    body += "You can also check your order history via our website. \n\n" +
                       "Contact Us: \n" +
                       "Phone: 082 648 7848 \n\n" +
                       "Address: \n" +
                       "602 Mount Batten Drive \n" +
                       "Resevoir Hills \n" +
                       "Durban \n\n" +
                       "The Alpha Unit \n" + DateTime.Now + "\n\n";

                    //  SendEmail(CartEmail, body, subject);
                    var SalonEm = new MailAddress("TrialApp69@gmail.com", "Ralenes Beauty Salon");
                    var email = new MailAddress(customerDetails.Email);
                    var pass = "Trial6921";
                    //var Sub = subject;
                    //var Bod = body;
                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(SalonEm.Address, pass)
                    };
                    using (var message = new MailMessage(SalonEm, email)
                    {
                        Subject = subject,
                        Body = body
                    })
                    {
                        smtp.Send(message);
                    }
                }
            }
            //EmailEnd

            return RedirectToAction(nameof(Payment),new { total = objMain.FinalPrice });
        }

        public ActionResult Payment(double total)
        {

            string url = "";
            double fTotal = total;
            //fTotal = Decimal.Ceiling(fTotal);
            url = "https://www.sandbox.paypal.com/cgi-bin/webscr?cmd=_xclick&amount=" + (fTotal) + "&business=JanjuaTailors@Shop.com&item_name=items&return=https://localhost:44389/Cart/PaymentView"; //localhost
            //url = "https://www.sandbox.paypal.com/cgi-bin/webscr?cmd=_xclick&amount=" + (fTotal) + "&business=JanjuaTailors@Shop.com&item_name=items&return=https://ralenesalonapp.azurewebsites.net/Cart/PaymentView"; //deploy

            return Redirect(url);


        }

        public ActionResult PaymentView()
        {

            return View();
        }


        [HttpPost]
        public ActionResult AddCart(int? Quan, int? ItemId)
        {
            if (!Quan.HasValue || !ItemId.HasValue)
                return View("NotFound");



            ShoppingCartMehods objMeth = new ShoppingCartMehods();
            string Result = objMeth.AddToCart(ItemId.Value, Quan.Value, int.Parse(Session["ID"].ToString()));
            if (Result == "Successful" || Result == "Over Quantity")
            {
                return RedirectToAction("CartView");
            }
            else
            {
                return View("NotFound");
            }
        }

        [HttpPost]
        public ActionResult MinusCart(int? ItemId)
        {
            if (!ItemId.HasValue)
                return View("NotFound");


            ShoppingCartMehods objMeth = new ShoppingCartMehods();
            if (objMeth.MinusFromCart(ItemId.Value, int.Parse(Session["ID"].ToString())))
            {
                return RedirectToAction("CartView");
            }
            else
            {
                return View("NotFound");
            }
        }
        [HttpPost]
        public ActionResult DeleteCart(int? ItemId)
        {
            if (!ItemId.HasValue)
                return View("NotFound");



            ShoppingCartMehods objMeth = new ShoppingCartMehods();
            if (objMeth.DeleteFromCart(ItemId.Value, int.Parse(Session["ID"].ToString())))
            {
                return RedirectToAction("CartView");
            }
            else
            {
                return View("NotFound");
            }
        }

        public ActionResult ViewOrderDetails(int? CustomerId, int? OrderId)
        {
            if (!CustomerId.HasValue || !OrderId.HasValue)
                return View();

            db = new SalonContext();
            var OrderObj = db.OrderItems.Include("OrderMain").Where(o => o.OrderMain.CustomerId == CustomerId.Value && o.OrderId == OrderId.Value).ToList();
            return View("Orders", OrderObj);

        }
        /*
         * Order Details: 1
         * 
         * Name | Quantity | Price | Total Price
         * Coke |    5     |  10   |    50
         * 
         * <Go back to Orders    
         */

        private void SendEmail(string emailAddress, string body, string subject)
        {

            try
            {

                /*
                using (MailMessage mm = new MailMessage("TrialApp69@gmail.com", emailAddress))
                {
                    mm.Subject = subject;
                    mm.Body = body;

                    mm.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;
                    smtp.UseDefaultCredentials = false;
                    NetworkCredential NetworkCred = new NetworkCredential("TrialApp69@gmail.com", "Trial6921");
                    smtp.Credentials = NetworkCred;
                    smtp.Port = 587;
                    smtp.Send(mm);
                }
                */

                var SalonEm = new MailAddress("TrialApp69@gmail.com", "Ralenes Beauty Salon");
                var email = new MailAddress(emailAddress);
                var pass = "Trial6921";
                //var Sub = subject;
                //var Bod = body;
                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(SalonEm.Address, pass)
                };
                using (var message = new MailMessage(SalonEm, email)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(message);
                }

            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }

        }
    }
}