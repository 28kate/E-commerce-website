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
    public class TempController : Controller
    {
        private SalonContext db = new SalonContext();

        // GET: Temp
        public ActionResult Index()
        {
            var tblAppointments = db.TblAppointments.Include(t => t.TblCustomerReg);
            return View(tblAppointments.ToList());
        }

        // GET: Temp/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblAppointments tblAppointments = db.TblAppointments.Find(id);
            if (tblAppointments == null)
            {
                return HttpNotFound();
            }
            return View(tblAppointments);
        }

        // GET: Temp/Create
        public ActionResult Create()
        {
            ViewBag.CustomerID = new SelectList(db.TblCustomerRegs, "CustomerID", "FirstName");
            return View();
        }

        // POST: Temp/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Time,Date,Treatment,CustomerID")] TblAppointments tblAppointments)
        {
            if (ModelState.IsValid)
            {
                db.TblAppointments.Add(tblAppointments);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerID = new SelectList(db.TblCustomerRegs, "CustomerID", "FirstName", tblAppointments.CustomerID);
            return View(tblAppointments);
        }

        // GET: Temp/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblAppointments tblAppointments = db.TblAppointments.Find(id);
            if (tblAppointments == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerID = new SelectList(db.TblCustomerRegs, "CustomerID", "FirstName", tblAppointments.CustomerID);
            return View(tblAppointments);
        }

        // POST: Temp/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Time,Date,Treatment,CustomerID")] TblAppointments tblAppointments)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblAppointments).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerID = new SelectList(db.TblCustomerRegs, "CustomerID", "FirstName", tblAppointments.CustomerID);
            return View(tblAppointments);
        }

        // GET: Temp/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblAppointments tblAppointments = db.TblAppointments.Find(id);
            if (tblAppointments == null)
            {
                return HttpNotFound();
            }
            return View(tblAppointments);
        }

        // POST: Temp/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TblAppointments tblAppointments = db.TblAppointments.Find(id);
            db.TblAppointments.Remove(tblAppointments);
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
