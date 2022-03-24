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

namespace SalonApp.Controllers
{
    public class AdminProcessRefundController : Controller
    {
        private SalonContext db = new SalonContext();

        // GET: AdminProcessRefund
        public ActionResult Index()   //LINQ To display all enteries where the deliverystatus="Requesting Refund"
        {
            var orderMain = db.OrderMain.Include(o => o.TblCustomerReg).Include(o => o.TblDriverForLogin).Where(o => o.deliverystatus == OrderMain.DeliveryStatus.Requesting_Refund);
            return View(orderMain.ToList());
        }


        public ActionResult AcceptRefund(int? id) //Send Email stating that their refund has been accepted
        {
           
            if (id == null) return HttpNotFound();

            OrderMain orderMain = db.OrderMain.Find(id.Value);

            orderMain.deliverystatus = OrderMain.DeliveryStatus.Refund_Accepted;
            
            db.SaveChanges();
           

            using (var context = new SalonContext())
            {
                if (orderMain != null)
                {
                    var getUser2 = context.TblCustomerRegs.Find(orderMain.CustomerId);

                    context.Configuration.ValidateOnSaveEnabled = false;
                    context.SaveChanges();

                    var subject = "Ralene's Beauty Salon: " +
                                  "Refund Approved";
                    var body = "Hi " + getUser2.FirstName + ",\n" +
                                  "Thank you for choosing Ralene's salon for your beauty needs. \n" +
                                  "You recently requested a refund on your purchase. \n\n" +
                                  "We have approved your request and will ensure PayPal refunds you. \n" +
                                  "Thank You. \n\n" +
                                  "Contact Us: \n" +
                                  "Phone: 082 648 7848 \n\n" +
                                  "Address: \n" +
                                  "602 Mount Batten Drive \n" +
                                  "Resevoir Hills \n" +
                                  "Durban \n\n" +
                                  "The Alpha Unit \n" + DateTime.Now + "\n\n";

                    SendEmail(getUser2.Email, body, subject);
                }
            }

            return RedirectToAction(nameof(Index));
        }


        public ActionResult DenyRefund(int? id)    //Send Email stating refund has been denied
        {
            if (id == null) return HttpNotFound();

            OrderMain orderMain = db.OrderMain.Find(id.Value);

            orderMain.deliverystatus = OrderMain.DeliveryStatus.Refund_Denied;

            db.SaveChanges();

            using (var context = new SalonContext())
            {
                if (orderMain != null)
                {
                    var getUser2 = context.TblCustomerRegs.Find(orderMain.CustomerId);

                    context.Configuration.ValidateOnSaveEnabled = false;
                    context.SaveChanges();

                    var subject = "Ralene's Beauty Salon: " +
                                  "Refund Denied";
                    var body = "Hi " + getUser2.FirstName + ",\n" +
                                  "Thank you for choosing Ralene's salon for your beauty needs. \n" +
                                  "You recently requested a refund on your purchase. \n\n" +
                                  "Unfortunately your refund request has been denied. \n" +
                                  "Feel free to follow up through the comments on our site or our email. \n" +
                                  "Thank You. \n\n" +
                                  "Contact Us: \n" +
                                  "Phone: 082 648 7848 \n\n" +
                                  "Address: \n" +
                                  "602 Mount Batten Drive \n" +
                                  "Resevoir Hills \n" +
                                  "Durban \n\n" +
                                  "The Alpha Unit \n" + DateTime.Now + "\n\n";

                    SendEmail(getUser2.Email, body, subject);
                }
            }


            return RedirectToAction(nameof(Index));
        }



        private void SendEmail(string emailAddress, string body, string subject)
        {
            if (ModelState.IsValid)
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


















        // GET: AdminProcessRefund/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderMain orderMain = db.OrderMain.Find(id);
            if (orderMain == null)
            {
                return HttpNotFound();
            }
            return View(orderMain);
        }

        // GET: AdminProcessRefund/Create
        public ActionResult Create()
        {
            ViewBag.CustomerId = new SelectList(db.TblCustomerRegs, "CustomerID", "FirstName");
            ViewBag.DriverId = new SelectList(db.tblDriverForLogins, "DriverLoginID", "Username");
            return View();
        }

        // POST: AdminProcessRefund/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CustomerId,DateOrdered,TimeOrdered,FinalPrice,Status,DriverId,deliveryDate,DeliveryAddress,DeliverySuburb,DeliveryZipCode,Delivery,deliverystatus,DeliveryID")] OrderMain orderMain)
        {
            if (ModelState.IsValid)
            {
                db.OrderMain.Add(orderMain);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerId = new SelectList(db.TblCustomerRegs, "CustomerID", "FirstName", orderMain.CustomerId);
            ViewBag.DriverId = new SelectList(db.tblDriverForLogins, "DriverLoginID", "Username", orderMain.DriverId);
            return View(orderMain);
        }

        // GET: AdminProcessRefund/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderMain orderMain = db.OrderMain.Find(id);
            if (orderMain == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerId = new SelectList(db.TblCustomerRegs, "CustomerID", "FirstName", orderMain.CustomerId);
            ViewBag.DriverId = new SelectList(db.tblDriverForLogins, "DriverLoginID", "Username", orderMain.DriverId);
            return View(orderMain);
        }

        // POST: AdminProcessRefund/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CustomerId,DateOrdered,TimeOrdered,FinalPrice,Status,DriverId,deliveryDate,DeliveryAddress,DeliverySuburb,DeliveryZipCode,Delivery,deliverystatus,DeliveryID")] OrderMain orderMain)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderMain).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerId = new SelectList(db.TblCustomerRegs, "CustomerID", "FirstName", orderMain.CustomerId);
            ViewBag.DriverId = new SelectList(db.tblDriverForLogins, "DriverLoginID", "Username", orderMain.DriverId);
            return View(orderMain);
        }

        // GET: AdminProcessRefund/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderMain orderMain = db.OrderMain.Find(id);
            if (orderMain == null)
            {
                return HttpNotFound();
            }
            return View(orderMain);
        }

        // POST: AdminProcessRefund/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderMain orderMain = db.OrderMain.Find(id);
            db.OrderMain.Remove(orderMain);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
