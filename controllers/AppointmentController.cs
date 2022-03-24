using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SalonApp.Models;
using System.Net;
using System.Net.Mail;
using System.Data.Entity;

namespace SalonApp.Controllers
{
    public class AppointmentController : Controller
    {
        SalonContext db = new SalonContext();
        // GET: Appointment
        public ActionResult Booking()
        {
            Session["BookConf"] = null;
            ViewBag.BookConf = null;
            return View();
        }

        public ActionResult AppointmentTableV()
        {
            int ID = int.Parse(Session["ID"].ToString());
            var ListAppointments = db.TblAppointments.Where(app=>app.CustomerID == ID).ToList();
            return View(ListAppointments);
        }



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

        // POST: Temp/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Time,Date,Treatment")] TblAppointments tblAppointments, TblAppointments obj)
        {
            int CusId = int.Parse(Session["ID"].ToString());

            tblAppointments.CustomerID = int.Parse(Session["TempID"].ToString());
            Session["TempID"] = null;
            if (ModelState.IsValid)
            {
                var isDateInDB = db.TblAppointments.Any(x => x.Date == obj.Date);
                var isTimeInDB = db.TblAppointments.Any(x => x.Time == obj.Time);
                //Checking if date and time exists

                if (isTimeInDB && isDateInDB)
                {
                    ModelState.AddModelError("Time", "Selected time is booked for the day");
                    return View(obj);
                }
                TblAppointments app2 = new TblAppointments();
                app2.Time = obj.Time;
                app2.Date = obj.Date;
                app2.Treatment = obj.Treatment.ToString();
                app2.CustomerID = CusId;

                
                using (var context = new SalonContext())
                {
                    var getUser2 = (from s in context.TblCustomerRegs where s.CustomerID == CusId select s).FirstOrDefault();
                    if (getUser2 != null)
                    {

                        context.Configuration.ValidateOnSaveEnabled = false;
                        context.SaveChanges();

                        var subject = "Ralene's Beauty Salon: " +
                                      "Your Rescheduled Appointment Details";
                        var body =    "Hi " + getUser2.FirstName + ",\n" +
                                      "Thank you for choosing Ralene's salon for your beauty needs. \n" +
                                      "You recently requested a change in your previous booking. \n\n" +
                                      "Your new booking is as follows: \n" +
                                      "Time: " + app2.Time + "\n" +
                                      "Date: " + app2.Date + "\n" +
                                      "Treatment: " + app2.Treatment + "\n\n" +
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
                


                db.Entry(tblAppointments).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("AppointmentTableV");
            }
            return View(tblAppointments);
        }

        // GET: Temp/Delete/5
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

        // POST: Temp/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
             TblAppointments tblAppointments = db.TblAppointments.Find(id);
             db.TblAppointments.Remove(tblAppointments);
             db.SaveChanges();
             return RedirectToAction("AppointmentTableV");
        }


        [HttpPost]

        public ActionResult Booking(Appointments obj)
        {
            int CusId = int.Parse(Session["ID"].ToString());

            if (ModelState.IsValid)
            {
                var isDateInDB = db.TblAppointments.Any(x => x.Date == obj.Date);
                var isTimeInDB = db.TblAppointments.Any(x => x.Time == obj.Time);
                //Checking if date and time exists
                
                if (isTimeInDB && isDateInDB)
                {
                    ModelState.AddModelError("Time", "Selected time is booked for the day");
                    return View(obj);
                }

                TblAppointments app = new TblAppointments();
                app.Time = obj.Time;
                app.Date = obj.Date;
                app.Treatment = obj.TreatmentList.ToString();
         

                app.CustomerID = int.Parse(Session["ID"].ToString());
                app.CustomerID = CusId;

                db.TblAppointments.Add(app);
                db.SaveChanges();

                
                using (var context = new SalonContext())
                {
                    var getUser = (from s in context.TblCustomerRegs where s.CustomerID == CusId select s).FirstOrDefault();
                    if (getUser != null)
                    {

                        context.Configuration.ValidateOnSaveEnabled = false;
                        context.SaveChanges();

                        var subject = "Ralene's Beauty Salon: " + 
                                      "Your Appointment Details";
                        var body =    "Hi " + getUser.FirstName + ",\n " +
                                      "Thank you for choosing Ralene's salon for your beauty needs. \n\n" +
                                      "Your booking is as follows: \n" +
                                      "Time: " + app.Time + "\n" +
                                      "Date: " + app.Date + "\n" +
                                      "Treatment: " + app.Treatment + "\n\n" +
                                      "Contact Us: \n" +
                                      "Phone: 082 648 7848 \n\n" +
                                      "Address: \n" +
                                      "602 Mount Batten Drive \n" +
                                      "Resevoir Hills \n" +
                                      "Durban \n\n" +
                                      "The Alpha Unit \n" + DateTime.Now + "\n\n";

                        SendEmail(getUser.Email, body, subject);
                    }
                }
                

                //Add session to reschedule?
                Session["BookConf"] = "Your booking is successful, an email will be sent to you shortly" + Session["UserName"];
                return RedirectToAction("CustomerHome", "CustomerHomeP");
            }

            ModelState.Clear();
            return View(obj);
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