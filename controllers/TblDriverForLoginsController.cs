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
    public class TblDriverForLoginsController : Controller
    {
        private SalonContext db = new SalonContext();

        // GET: TblDriverForLogins
        public ActionResult Index()
        {
            return View(db.tblDriverForLogins.ToList());
        }

        // GET: TblDriverForLogins/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblDriverForLogin tblDriverForLogin = db.tblDriverForLogins.Find(id);
            if (tblDriverForLogin == null)
            {
                return HttpNotFound();
            }
            return View(tblDriverForLogin);
        }

        // GET: TblDriverForLogins/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TblDriverForLogins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DriverLoginID,Username,Password")] TblDriverForLogin tblDriverForLogin)
        {
            if (ModelState.IsValid)
            {
                db.tblDriverForLogins.Add(tblDriverForLogin);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblDriverForLogin);
        }

        // GET: TblDriverForLogins/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblDriverForLogin tblDriverForLogin = db.tblDriverForLogins.Find(id);
            if (tblDriverForLogin == null)
            {
                return HttpNotFound();
            }
            return View(tblDriverForLogin);
        }

        // POST: TblDriverForLogins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DriverLoginID,Username,Password")] TblDriverForLogin tblDriverForLogin)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblDriverForLogin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblDriverForLogin);
        }

        // GET: TblDriverForLogins/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblDriverForLogin tblDriverForLogin = db.tblDriverForLogins.Find(id);
            if (tblDriverForLogin == null)
            {
                return HttpNotFound();
            }
            return View(tblDriverForLogin);
        }

        // POST: TblDriverForLogins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TblDriverForLogin tblDriverForLogin = db.tblDriverForLogins.Find(id);
            db.tblDriverForLogins.Remove(tblDriverForLogin);
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
