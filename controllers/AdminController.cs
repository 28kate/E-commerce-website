using SalonApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SalonApp.Controllers
{
    public class AdminController : Controller
    {
        private SalonContext db;
        // GET: Admin
        public ActionResult AdminLogin()
        {
            return View();
        }

        public ActionResult AdminOrders()
        {          
            db = new SalonContext();
            List<OrderMain> objOM = db.OrderMain.Include("TblCustomerReg").ToList();
            return View("Orders", objOM);
        }

        public ActionResult OrderDetails(int? id)
        {
            if (!id.HasValue) return RedirectToAction("AdminOrders");

            db = new SalonContext();
            List<OrderItems> objItems = db.OrderItems.Where(o => o.OrderId == id.Value).ToList();
            return View(objItems);
        }

       

        [HttpPost]
        public ActionResult SearchOrder(DateTime? datepicker)
        {
            if (datepicker == null)
                return RedirectToAction("AdminOrders");
            db = new SalonContext();
            List<OrderMain> objOM = db.OrderMain.Include("TblCustomerReg").Where(o=> DbFunctions.TruncateTime(o.DateOrdered) == DbFunctions.TruncateTime(datepicker.Value)).ToList();
            return View("Orders", objOM);
        }

        
    }
}