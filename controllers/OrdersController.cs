﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SalonApp.Controllers
{
    public class OrdersController : Controller
    {
        // GET: Orders
        public ActionResult Orders()
        {
            return View();
        }
    }
}