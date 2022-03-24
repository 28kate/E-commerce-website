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
    public class AdminPendingDeliveriesController : Controller
    {
        private SalonContext db = new SalonContext();

        // GET: AdminPendingDeliveries
        public ActionResult Index()
        {
            var orderMain = db.OrderMain.Include(o => o.TblCustomerReg).Include(o => o.TblDriverForLogin).Where(o => o.deliverystatus == OrderMain.DeliveryStatus.Processing);
            return View(orderMain);
        }

        // GET: AdminPendingDeliveries/Details/5
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

        // GET: AdminPendingDeliveries/Create
        public ActionResult Create()
        {
            ViewBag.CustomerId = new SelectList(db.TblCustomerRegs, "CustomerID", "FirstName");
            ViewBag.DriverId = new SelectList(db.tblDriverForLogins, "DriverLoginID", "Username");
            return View();
        }

        // POST: AdminPendingDeliveries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CustomerId,DateOrdered,TimeOrdered,FinalPrice,Status,DriverId,deliveryDate,DeliveryAddress,DeliverySuburb,DeliveryZipCode,Delivery,deliverystatus,DeliveryID")] OrderMain orderMain)
        {
            if (ModelState.IsValid)
            {
                db.OrderMain.Add(orderMain);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerId = new SelectList(db.TblCustomerRegs, "CustomerID", "FirstName", orderMain.CustomerId);
            ViewBag.DriverId = new SelectList(db.tblDriverForLogins, "DriverLoginID", "Username", orderMain.DriverId);
            return View(orderMain);
        }

        // GET: AdminPendingDeliveries/Edit/5
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
            ViewBag.DriverId = new SelectList(db.tblDriverForLogins, "DriverLoginID", "Username", orderMain.DriverId);
            return View(orderMain);
        }

        // POST: AdminPendingDeliveries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CustomerId,DateOrdered,TimeOrdered,FinalPrice,Status,DriverId,deliveryDate,DeliveryAddress,DeliverySuburb,DeliveryZipCode,Delivery,deliverystatus,DeliveryID")] OrderMain orderMain)
        {
            orderMain.deliverystatus = OrderMain.DeliveryStatus.Accepted;
            if (ModelState.IsValid)
            {
                db.Entry(orderMain).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerId = new SelectList(db.TblCustomerRegs, "CustomerID", "FirstName", orderMain.CustomerId);
            ViewBag.DriverId = new SelectList(db.tblDriverForLogins, "DriverLoginID", "Username", orderMain.DriverId);
            return View(orderMain);
        }

        // GET: AdminPendingDeliveries/Delete/5
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

        // POST: AdminPendingDeliveries/Delete/5
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
