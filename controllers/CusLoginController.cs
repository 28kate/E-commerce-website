using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using SalonApp.Models;


namespace SalonApp.Controllers
{
    public class CusLoginController : Controller
    {
        SalonContext db = new SalonContext();
        // GET: CusLogin
        public ActionResult LoginC()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LoginC([Bind(Include = "UserName, Password")]TblCustomerReg customer)
        {

            using (SalonContext db = new SalonContext())
            {


                var obj = db.TblCustomerRegs.Where(x => x.UserName.Equals(customer.UserName) && x.Password.Equals(customer.Password) && x.IsAdmin.Equals(false)).FirstOrDefault();
                if (obj != null)
                {
                    Session["ID"] = obj.CustomerID;
                    Session["UserName"] = obj.UserName.ToString();
                    Session["Password"] = obj.Password.ToString();
                    Session["LogConf"] = "You have succesfully logged in " + Session["UserName"].ToString().ToUpper() + "!";
                    Session["TypeUser"] = "Customer"; 
                    return RedirectToAction("CustomerHome", "CustomerHomeP");


                }
                else
                {
                    var objAdmin = db.TblAdmins.Where(x => x.UserName.Equals(customer.UserName) && x.Password.Equals(customer.Password)).FirstOrDefault();
                    if (objAdmin != null)
                    {
                        Session["ID"] = objAdmin.AdminID;
                        Session["UserName"] = objAdmin.UserName.ToString();
                        Session["Password"] = objAdmin.Password.ToString();
                        Session["AdminConf"] = "You have successfully logged in" + Session["UserName"].ToString().ToUpper() + "!";
                        Session["TypeUser"] = "Admin";

                        return RedirectToAction("AdminLogin", "Admin");

                    }

                    else
                    {
                        var objDriver = db.tblDriverForLogins.Where(x => x.Username.Equals(customer.UserName) && x.Password.Equals(customer.Password)).FirstOrDefault();
                        if (objDriver != null)
                        {

                            Session["ID"] = objDriver.DriverLoginID;
                            Session["UserName"] = objDriver.Username.ToString();
                            Session["Password"] = objDriver.Password.ToString();
                            Session["DriverConf"] = "You have successfully logged in" + Session["UserName"].ToString().ToUpper() + "!";
                            Session["TypeUser"] = "Driver";

                            return RedirectToAction("DriverHomePage", "DriverHomePage");
                        }
                    }






                }

               







            }



            ModelState.Clear();
            ViewBag.LogConf = "Invalid User";
            return View();

        }


        public ActionResult LogOut()
        {
            Session["ID"] = "";
            Session["UserName"] = "";
            Session["Password"] = "";
            Session["TypeUser"] = "";

            return RedirectToAction("LoginC");
        }

        // Forgot Password Changes

        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(string EmailID)
        {
            string resetCode = Guid.NewGuid().ToString();
            var verifyUrl = "/Cuslogin/ResetPassword/" + resetCode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);
            using (var context = new SalonContext())
            {
                var getUser = (from s in context.TblCustomerRegs where s.Email == EmailID select s).FirstOrDefault();
                if (getUser != null)
                {
                    getUser.ResetPasswordCode = resetCode;



                    context.Configuration.ValidateOnSaveEnabled = false;
                    context.SaveChanges();

                    var subject = "Ralene's Beauty Salon: " +
                                  "Password Reset Request";
                    var body =    "Hi " + getUser.FirstName + ",\n" + 
                                  "You recently requested to reset your password for your account. Click the link below to reset it. " +
                                  "\n\n <a href='" + link + "'>" + link + "</a> \n\n" +
                                  "If you did not request a password reset, please ignore this email or reply to let us know.\n\n Thank you. \n\n" +
                                  "Contact Us: \n" +
                                  "Phone: 082 648 7848 \n\n" +
                                  "Address: \n" +
                                  "602 Mount Batten Drive \n" +
                                  "Resevoir Hills \n" +
                                  "Durban \n\n" +
                                  "The Alpha Unit \n " + DateTime.Now + "\n\n"; ;

                    SendEmail(getUser.Email, body, subject);

                    ViewBag.Message = "Reset password link has been sent to your email address.";
                }
                else
                {
                    ViewBag.Message = "User doesn't exist.";
                    return View();
                }
            }

            return View();
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

        public ActionResult ResetPassword(string id)
        {

            if (string.IsNullOrWhiteSpace(id))
            {
                return HttpNotFound();
            }

            using (var context = new SalonContext())
            {
                var user = context.TblCustomerRegs.Where(a => a.ResetPasswordCode == id).FirstOrDefault();
                if (user != null)
                {
                    ResetPasswordModel model = new ResetPasswordModel();
                    model.ResetCode = id;
                    return View(model);
                }
                else
                {
                    return HttpNotFound();
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(ResetPasswordModel model)
        {
            var message = "";
            if (ModelState.IsValid)
            {
                using (var context = new SalonContext())
                {
                    var user = context.TblCustomerRegs.Where(a => a.ResetPasswordCode == model.ResetCode).FirstOrDefault();
                    if (user != null)
                    {

                        user.Password = model.NewPassword;

                        user.ResetPasswordCode = "";

                        context.Configuration.ValidateOnSaveEnabled = false;
                        context.SaveChanges();
                        message = "New password updated successfully";
                    }
                }
            }
            else
            {
                message = "Something Is Incorrect";
            }
            ViewBag.Message = message;
            return View(model);
        }

    }
}
