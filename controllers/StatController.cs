using SalonApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SalonApp.Controllers
{
    public class StatController : Controller
    {
        private readonly SalonContext db = new SalonContext();
        // GET: Stat
        public ActionResult Index(int? id)
        {
            //eg.
            //Start
            if (id == null)
                return HttpNotFound();
            var MoneyForItem = db.OrderItems.Include("OrderMain").Where(o => o.Id == id.Value && o.OrderId == o.OrderMain.Id && o.OrderMain.Status == "Successful").ToList();
                //End
            return View(MoneyForItem);
        }
    }
}