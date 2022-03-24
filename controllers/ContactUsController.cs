using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Net;
using System.Net.Mail;
using SalonApp.Models;


namespace SalonApp.Controllers
{
    public class ContactUsController : Controller
    {
        // GET: ContactUs
        public ActionResult ContactUs()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ContactUs(ContactUsEmail model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    /*
                    using (MailMessage mm = new MailMessage(model.Email, model.To))
                     {
                        mm.Subject = model.Subject;
                     mm.Body = model.Body;
                          */
                    SalonContext db = new SalonContext();
                    int CusId = int.Parse(Session["ID"].ToString());
                    var getCart = (from s in db.TblCustomerRegs where s.CustomerID == CusId select s).FirstOrDefault();
                    model.Subject = getCart.FirstName + " " + getCart.Surname + " - " + model.Subject;
                    model.Body = "Customer Email: " + getCart.Email + "\n\n" + model.Body;
                    var SalonEm = new MailAddress("TrialApp69@gmail.com", "Ralenes Beauty Salon");
                    var pass = "Trial6921";
                    var email = new MailAddress("TrialApp69@gmail.com");
                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = true,
                        Credentials = new NetworkCredential(SalonEm.Address, pass)

                    };
                    using (var message = new MailMessage(SalonEm,email)
                    {
                        Subject = model.Subject,
                        Body = model.Body
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
            return View();
        }

    }
}