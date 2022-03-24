using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SalonApp.Models;

namespace SalonApp.Controllers
{
    public class CusRegController : Controller
    {
        SalonContext db = new SalonContext();
        // GET: CusReg
        public ActionResult Register ()
        {
            Session["WelConf"] = null;
            ViewBag.RegConf = null;
            return View();
        }

        public ActionResult CustomerV()
        {
            var ListCustomer = db.TblCustomerRegs.ToList();
            return View(ListCustomer);
        }

        [HttpPost]

        public ActionResult Register (CustomerReg obj)
        {
            if (ModelState.IsValid)
            {

                var isUserNameExisting = db.TblCustomerRegs.Any(x => x.UserName == obj.UserName);
                if (isUserNameExisting)
                {
                    ModelState.AddModelError("Username", "User With This Name Already Exists");
                    return View(obj);
                }

                var isEmailExisting = db.TblCustomerRegs.Any(x => x.Email == obj.Email);
                if (isEmailExisting)
                {
                    ModelState.AddModelError("Email", "User With This Email Already Exists");
                    return View(obj);
                }

                var isPhoneExisting = db.TblCustomerRegs.Any(x => x.PhoneNumber == obj.PhoneNumber);
                if (isPhoneExisting)
                {
                    ModelState.AddModelError("PhoneNumber", "User With This Number Already Exists");
                    return View(obj);
                }

                var isPasswordExisting = db.TblCustomerRegs.Any(x => x.Password == obj.Password);
                if (isPasswordExisting)
                {
                    ModelState.AddModelError("Password", "User With This Password Already Exists");
                    return View(obj);
                }



                TblCustomerReg cus = new TblCustomerReg();
                    cus.FirstName = obj.FirstName;
                    cus.Surname = obj.Surname;
                    cus.Email = obj.Email;
                    cus.PhoneNumber = obj.PhoneNumber;
                    cus.UserName = obj.UserName;
                    cus.Password = obj.Password;
                    db.TblCustomerRegs.Add(cus);
                    db.SaveChanges();

                
                Session["WelConf"] = "You have succesfully Registered!" + Session["UserName"];
                return RedirectToAction("LoginC", "CusLogin");

            }

            ModelState.Clear();
            return View(obj);


        }

    }
}

