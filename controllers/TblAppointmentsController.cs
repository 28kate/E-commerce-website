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
    public class TblAppointmentsController : Controller
    {
        private SalonContext db = new SalonContext();

        // GET: TblAppointments
        public ActionResult Index()
        {
            return View(db.TblAppointments.ToList());
        }

        [HttpPost]
        public ActionResult Index(DateTime? datepicker)
        {
            if (datepicker == null)
                return RedirectToAction("Index");
            var ListAppointments =  db.TblAppointments.Where(app => DbFunctions.TruncateTime(app.Date) == DbFunctions.TruncateTime(datepicker.Value)).ToList();
            return View("Index", ListAppointments);
        }

        // GET: TblAppointments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblAppointments tblAppointments = db.TblAppointments.Find(id);
            if (tblAppointments == null)
            {
                return HttpNotFound();
            }
            return View(tblAppointments);
        }

        // GET: TblAppointments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TblAppointments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Time,Date,Treatment")] TblAppointments tblAppointments)
        {
            if (ModelState.IsValid)
            {
                db.TblAppointments.Add(tblAppointments);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblAppointments);
        }

        // GET: TblAppointments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblAppointments tblAppointments = db.TblAppointments.Find(id);
            if (tblAppointments == null)
            {
                return HttpNotFound();
            }
            return View(tblAppointments);
        }

        // POST: TblAppointments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Time,Date,Treatment")] TblAppointments tblAppointments)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblAppointments).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblAppointments);
        }

        // GET: TblAppointments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblAppointments tblAppointments = db.TblAppointments.Find(id);
            if (tblAppointments == null)
            {
                return HttpNotFound();
            }
            return View(tblAppointments);
        }

        // POST: TblAppointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            if (ModelState.IsValid)
            {
                using (var context = new SalonContext())
                {
                    var objApp = context.TblAppointments.Find(id);
                    if (objApp != null)
                    {
                        var getUser = context.TblCustomerRegs.Find(objApp.CustomerID);

                        context.Configuration.ValidateOnSaveEnabled = false;
                        context.SaveChanges();

                        var subject = "Ralene's Beauty Salon: " +
                                      "We're sorry for the inconvenience";
                        var body =    "Hi " + getUser.FirstName + ",\n " +
                                      "Thank you for choosing Ralene's salon for your beauty needs.\n" +
                                      "Unfortunately your booking has been cancelled due to unforseen circumstances. \n\n" +
                                      "Please book with us again in the future!\n" +
                                      "Your support means the world to us!\n\n" +
                                      "Address: \n" +
                                      "602 Mount Batten Drive \n" +
                                      "Resevoir Hills \n" +
                                      "Durban \n\n" +
                                      "The Alpha Unit \n" + DateTime.Now + "\n\n";

                        SendEmail(getUser.Email, body, subject);
                    }

                }
            }

            TblAppointments tblAppointments = db.TblAppointments.Find(id);
            db.TblAppointments.Remove(tblAppointments);
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
    }
}
