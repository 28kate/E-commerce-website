using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SalonApp.Models;
using SalonApp.Views;


namespace SalonApp.Controllers
{
    public class ArticleCommentsController : Controller
    {
        private SalonContext db = new SalonContext();

        // GET: ArticleComments
        public  ActionResult Index()
          {
             var articleComments = db.ArticleComments.Include(a => a.Articles);
          return View(articleComments.ToList());
           }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(ArticleCommentViewModel vm)
        {
            var comment = vm.Comments;
            var articleId = vm.ArticleId;
            var rating = vm.Rating;

            ArticleComment ArtComment = new ArticleComment()
            {
                ArticlesId = articleId,
                Comments = comment,
                Rating = rating,
                PublishedDate = DateTime.Now
            };
            db.ArticleComments.Add(ArtComment);
            db.SaveChanges();

            return RedirectToAction("Details", "Articles", new { id = articleId });
        }
    



        // GET: ArticleComments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArticleComment articleComment = db.ArticleComments.Find(id);
            if (articleComment == null)
            {
                return HttpNotFound();
            }
            return View(articleComment);
        }

        // GET: ArticleComments/Create
        public ActionResult Create()
        {
            ViewBag.ArticlesId = new SelectList(db.Articles, "Id", "Title");
            return View();
        }

        // POST: ArticleComments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Comments,publishedDate,ArticlesId,Rating")] ArticleComment articleComment)
        {
            if (ModelState.IsValid)
            {
                db.ArticleComments.Add(articleComment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ArticlesId = new SelectList(db.Articles, "Id", "Title", articleComment.ArticlesId);
            return View(articleComment);
        }

        // GET: ArticleComments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArticleComment articleComment = db.ArticleComments.Find(id);
            if (articleComment == null)
            {
                return HttpNotFound();
            }
            ViewBag.ArticlesId = new SelectList(db.Articles, "Id", "Title", articleComment.ArticlesId);
            return View(articleComment);
        }

        // POST: ArticleComments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Comments,publishedDate,ArticlesId,Rating")] ArticleComment articleComment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(articleComment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ArticlesId = new SelectList(db.Articles, "Id", "Title", articleComment.ArticlesId);
            return View(articleComment);
        }

        // GET: ArticleComments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArticleComment articleComment = db.ArticleComments.Find(id);
            if (articleComment == null)
            {
                return HttpNotFound();
            }
            return View(articleComment);
        }

        // POST: ArticleComments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ArticleComment articleComment = db.ArticleComments.Find(id);
            db.ArticleComments.Remove(articleComment);
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
