using SalonApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SalonApp.Controllers
{
    public class ChartController : Controller
    {
        private readonly SalonContext db = new SalonContext();
        // GET: Chart
        public ActionResult Index()
        {
            var TblOrdersList = db.TblOrders.ToList();
            return View(TblOrdersList);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblOrders tblOrders = db.TblOrders.Find(id.Value);
            if (tblOrders == null)
            {
                return HttpNotFound();
            }
            return View(tblOrders);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TblOrders tblOrders = db.TblOrders.Find(id);
            db.TblOrders.Remove(tblOrders);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}