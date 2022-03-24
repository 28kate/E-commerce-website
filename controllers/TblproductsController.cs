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
    public class TblproductsController : Controller
    {
        private SalonContext db = new SalonContext();

        // GET: Tblproducts
        public ActionResult Index()
        {
            return View(db.Tblproducts.ToList());
        }

        // GET: Tblproducts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tblproduct tblproduct = db.Tblproducts.Find(id);
            if (tblproduct == null)
            {
                return HttpNotFound();
            }
            return View(tblproduct);
        }

        // GET: Tblproducts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tblproducts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProdID,ProdName,ProdPrice,ProdDescrip,ProdQuantity")] Tblproduct tblproduct)
        {
            if (ModelState.IsValid)
            {
                db.Tblproducts.Add(tblproduct);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblproduct);
        }

        // GET: Tblproducts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tblproduct tblproduct = db.Tblproducts.Find(id);
            if (tblproduct == null)
            {
                return HttpNotFound();
            }
            return View(tblproduct);
        }

        // POST: Tblproducts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProdID,ProdName,ProdPrice,ProdDescrip,ProdQuantity")] Tblproduct tblproduct)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblproduct).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblproduct);
        }

        // GET: Tblproducts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tblproduct tblproduct = db.Tblproducts.Find(id);
            if (tblproduct == null)
            {
                return HttpNotFound();
            }
            return View(tblproduct);
        }

        // POST: Tblproducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tblproduct tblproduct = db.Tblproducts.Find(id);
            db.Tblproducts.Remove(tblproduct);
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
