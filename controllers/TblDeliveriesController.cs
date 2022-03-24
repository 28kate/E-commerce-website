using SalonApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SalonApp.Controllers
{
    public class TblDeliveriesController : Controller
    {

        private SalonContext db = new SalonContext();
        // GET: TblDeliveries
        public ActionResult DisplayDeliveries()
        {
            return View();
        }


        public ActionResult IndexDeliveries()
        {
            return View(db.OrderMain.ToList());

        }


       



        


    }







   

}