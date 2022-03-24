using SalonApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SalonApp.Controllers
{
    public class WelcomeController : Controller
    {
        // GET: Welcome
        public ActionResult Welcome()
        {
            using (SalonContext db = new SalonContext())
            {

                if (!db.TblAdmins.Any())
                {

                    


                }

                //var checkAdmin = ...;
                // if ()
                //  {
                // Go to the admin reg view
                // }
            }

            return View();
        }
    }
}