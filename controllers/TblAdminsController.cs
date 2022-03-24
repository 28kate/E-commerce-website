using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SalonApp.Models;
using System.Net.Mail;

namespace SalonApp.Controllers
{
    public class TblAdminsController : Controller
    {
        private SalonContext db = new SalonContext();

        // GET: TblAdmins
        public ActionResult Index()
        {
            return View(db.TblAdmins.ToList());
        }

        // GET: TblAdmins/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblAdmin tblAdmin = db.TblAdmins.Find(id);
            if (tblAdmin == null)
            {
                return HttpNotFound();
            }
            return View(tblAdmin);
        }

        // GET: TblAdmins/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TblAdmins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AdminID,FirstName,Surname,Email,PhoneNumber,UserName,Password,ResetPasswordCode")] TblAdmin tblAdmin)
        {
            if (ModelState.IsValid)
            {
                db.TblAdmins.Add(tblAdmin);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblAdmin);
        }

        // GET: TblAdmins/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblAdmin tblAdmin = db.TblAdmins.Find(id);
            if (tblAdmin == null)
            {
                return HttpNotFound();
            }
            return View(tblAdmin);
        }

        // POST: TblAdmins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AdminID,FirstName,Surname,Email,PhoneNumber,UserName,Password,ResetPasswordCode")] TblAdmin tblAdmin)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblAdmin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblAdmin);
        }

        // GET: TblAdmins/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblAdmin tblAdmin = db.TblAdmins.Find(id);
            if (tblAdmin == null)
            {
                return HttpNotFound();
            }
            return View(tblAdmin);
        }

        // POST: TblAdmins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, string EmailIDA)
        {

            TblAdmin tblAdmin = db.TblAdmins.Find(id);
            db.TblAdmins.Remove(tblAdmin);
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
