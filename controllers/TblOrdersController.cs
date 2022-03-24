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
    public class TblOrdersController : Controller
    {
        private SalonContext db = new SalonContext();

        // GET: TblOrders
        public ActionResult Index()
        {
            return View(db.TblOrders.ToList());
        }

        // GET: TblOrders/Details/5
        public ActionResult Details(int? id)
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

        // GET: TblOrders/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TblOrders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Price,Desc,Quantity")] TblOrders tblOrders, HttpPostedFileBase image1)
        {
            if (image1 == null)
            {
                ModelState.AddModelError("Image", "Please add an Image");
                return View(tblOrders);
            }
            else
            {
                tblOrders.Image = new byte[image1.ContentLength];
                image1.InputStream.Read(tblOrders.Image, 0, image1.ContentLength);
            }
            if (ModelState.IsValid)
            {
                db.TblOrders.Add(tblOrders);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblOrders);
        }

        // GET: TblOrders/Edit/5
        public ActionResult Edit(int? id)
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

        // POST: TblOrders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Price,Desc,Quantity")] TblOrders tblOrders, HttpPostedFileBase image1)
        {
            if (image1 == null)
            {
                tblOrders.Image = db.TblOrders.Find(tblOrders.Id).Image;
            }
            else
            {
                tblOrders.Image = new byte[image1.ContentLength];
                image1.InputStream.Read(tblOrders.Image, 0, image1.ContentLength);
            }
            if (ModelState.IsValid)
            {
                TblOrders objOrd = db.TblOrders.Find(tblOrders.Id);
                objOrd.Name = tblOrders.Name;
                objOrd.Desc = tblOrders.Desc;
                objOrd.Price = tblOrders.Price;
                objOrd.Quantity = tblOrders.Quantity;
                objOrd.Image = tblOrders.Image;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblOrders);
        }

        // GET: TblOrders/Delete/5
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

        // POST: TblOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TblOrders tblOrders = db.TblOrders.Find(id);
            db.TblOrders.Remove(tblOrders);
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
