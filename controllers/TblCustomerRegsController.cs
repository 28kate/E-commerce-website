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

    //db error
    //protected override void OnModelCreating(DbModelBuilder modelBuilder)
  //  {
      //  Database.SetInitializer<TblCustomerReg>(null);
     //   base.OnModelCreating(modelBuilder);
   // }




    public class TblCustomerRegsController : Controller
    {
        private SalonContext db = new SalonContext();

        // GET: TblCustomerRegs
        public ActionResult Index()
        {
            return View(db.TblCustomerRegs.ToList());
        }

        // GET: TblCustomerRegs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblCustomerReg tblCustomerReg = db.TblCustomerRegs.Find(id);
            if (tblCustomerReg == null)
            {
                return HttpNotFound();
            }
            return View(tblCustomerReg);
        }

        // GET: TblCustomerRegs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TblCustomerRegs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerID,FirstName,Surname,Email,PhoneNumber,UserName,Password")] TblCustomerReg tblCustomerReg)
        {
            if (ModelState.IsValid)
            {
                db.TblCustomerRegs.Add(tblCustomerReg);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblCustomerReg);
        }

        // GET: TblCustomerRegs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblCustomerReg tblCustomerReg = db.TblCustomerRegs.Find(id);
            if (tblCustomerReg == null)
            {
                return HttpNotFound();
            }
            return View(tblCustomerReg);
        }

        // POST: TblCustomerRegs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerID,FirstName,Surname,Email,PhoneNumber,UserName,Password,IsAdmin")] TblCustomerReg tblCustomerReg)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblCustomerReg).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblCustomerReg);
        }

        // GET: TblCustomerRegs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblCustomerReg tblCustomerReg = db.TblCustomerRegs.Find(id);
            if (tblCustomerReg == null)
            {
                return HttpNotFound();
            }
            return View(tblCustomerReg);
        }

        // POST: TblCustomerRegs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TblCustomerReg tblCustomerReg = db.TblCustomerRegs.Find(id);
            db.TblCustomerRegs.Remove(tblCustomerReg);
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
