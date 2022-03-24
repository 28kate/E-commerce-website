using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SalonApp.Models;

namespace SalonApp.Controllers
{
    public class DriverDeliveryConfirmController : Controller
    {
        private SalonContext db = new SalonContext();

        // GET: DriverDeliveryConfirm
        public ActionResult Index()                                           //LINQ Needed to display deliveries where of the same driver name that is logged in and where DeliveryStaus ="Accepted"
        {
            int DriverId = int.Parse(Session["ID"].ToString());
            var orderMain = db.OrderMain.Include(o => o.TblCustomerReg).Include(o => o.TblDriverForLogin)
                .Where(o=>o.DriverId == DriverId && o.deliverystatus == OrderMain.DeliveryStatus.Accepted).ToList();
            return View(orderMain);
        }


        public ActionResult AcceptDelivery(int? id)
        {
            if (id == null) return HttpNotFound();

            OrderMain orderMain = db.OrderMain.Find(id.Value);

            orderMain.deliverystatus = OrderMain.DeliveryStatus.En_Route;

            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }















        // GET: DriverDeliveryConfirm/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderMain orderMain = db.OrderMain.Find(id);
            if (orderMain == null)
            {
                return HttpNotFound();
            }
            return View(orderMain);
        }

        // GET: DriverDeliveryConfirm/Create
        public ActionResult Create()
        {
            ViewBag.CustomerId = new SelectList(db.TblCustomerRegs, "CustomerID", "FirstName");
            return View();
        }

        // POST: DriverDeliveryConfirm/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CustomerId,DateOrdered,TimeOrdered,FinalPrice,Status,deliveryDate,DeliveryAddress,DeliverySuburb,DeliveryZipCode,Delivery,Driver,deliverystatus,DeliveryID")] OrderMain orderMain)
        {
            if (ModelState.IsValid)
            {
                db.OrderMain.Add(orderMain);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerId = new SelectList(db.TblCustomerRegs, "CustomerID", "FirstName", orderMain.CustomerId);
            return View(orderMain);
        }

        // GET: DriverDeliveryConfirm/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderMain orderMain = db.OrderMain.Find(id);
            if (orderMain == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerId = new SelectList(db.TblCustomerRegs, "CustomerID", "FirstName", orderMain.CustomerId);
            return View(orderMain);
        }

        // POST: DriverDeliveryConfirm/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CustomerId,DateOrdered,TimeOrdered,FinalPrice,Status,deliveryDate,DeliveryAddress,DeliverySuburb,DeliveryZipCode,Delivery,Driver,deliverystatus,DeliveryID")] OrderMain orderMain)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderMain).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerId = new SelectList(db.TblCustomerRegs, "CustomerID", "FirstName", orderMain.CustomerId);
            return View(orderMain);
        }

        // GET: DriverDeliveryConfirm/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderMain orderMain = db.OrderMain.Find(id);
            if (orderMain == null)
            {
                return HttpNotFound();
            }
            return View(orderMain);
        }

        // POST: DriverDeliveryConfirm/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderMain orderMain = db.OrderMain.Find(id);
            db.OrderMain.Remove(orderMain);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
