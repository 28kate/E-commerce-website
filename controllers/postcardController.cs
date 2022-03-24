using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SalonApp.Controllers
{
    public class postcardController : Controller
    {
        // GET: postcard
        public ActionResult Index()
        {
            return View();
        }
    }
}