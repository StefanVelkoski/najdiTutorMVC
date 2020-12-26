using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FindTutorMVC.Models;

namespace FindTutorMVC.Controllers
{
    public class ReviewsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Reviews
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            List<Review> Reviews = db.Reviews
                .Include(r => r.Announcement)
                .Include(r => r.Submitter)
                .ToList();

            return View(Reviews);
        }

        [Authorize(Roles = "Admin, Customer")]
        public ActionResult MyReviews(string id)
        {
            List<ReviewToUser> ReviewsToUser = db.ReviewsToUsers
                .Where(ru => ru.UserId.Equals(id))
                .ToList();

            List<Review> reviews = db.Reviews
                .Include(r => r.Announcement)
                .Include(r => r.Submitter)
                .ToList();

            List<Review> MyReviews = new List<Review>();

            ReviewsToUser.ForEach(ru => {
                MyReviews.Add(reviews
                    .Find(r => r.Id == ru.ReviewId));
            });

            return View(MyReviews);
        }

        // GET: Reviews/Details/5
        [Authorize(Roles = "Admin, Customer")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            List<Review> reviews = db.Reviews
                .Include(r => r.Announcement)
                .Include(r => r.Submitter)
                .ToList();

            Review review = null;

            for (int i = 0; i < reviews.Count; i++)
            {
                if (reviews[i].Id == id)
                {
                    review = reviews[i];
                    break;
                }
            }
            
            if (review == null)
            {
                return HttpNotFound();
            }
            
            return View(review);
        }

        // GET: Reviews/Create
        [Authorize(Roles = "Admin, Customer")]
        public ActionResult Create(string id, int announcement)
        {
            ReviewForAnnouncement model = new ReviewForAnnouncement();
            model.UserId = id;
            model.AnnouncementId = announcement;
            model.Review = new Review();
            return View(model);
        }

        // POST: Reviews/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin, Customer")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId, AnnouncementId, Review")] ReviewForAnnouncement model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser Customer = db.Users.Find(model.UserId);
                Announcement announcement = db.Announcements.Find(model.AnnouncementId);
                Review review = new Review();
                review.Announcement = announcement;
                review.Submitter = Customer;
                review.Score = model.Review.Score;
                review.Reason = model.Review.Reason;

                int score = model.Review.Score;
                announcement.Score += score;

                List<Review> reviews = db.Reviews.Where(r => r.Announcement.Id == announcement.Id).ToList();
                if (reviews.Count != 0)
                {
                    announcement.Score /= (reviews.Count + 1);
                }

                review = db.Reviews.Add(review);
                db.SaveChanges();

                ReviewToUser ReviewToUser = new ReviewToUser();
                ReviewToUser.UserId = model.UserId;
                ReviewToUser.ReviewId = review.Id;
                db.ReviewsToUsers.Add(ReviewToUser);
                db.SaveChanges();

                return RedirectToAction("MyReviews/" + model.UserId);
            }

            return View(model);
        }

        // GET: Reviews/Edit/5
        [Authorize(Roles = "Admin, Customer")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            List<Review> reviews = db.Reviews
                .Include(r => r.Announcement)
                .Include(r => r.Submitter)
                .ToList();

            Review review = null;

            for (int i = 0; i < reviews.Count; i++)
            {
                if (reviews[i].Id == id)
                {
                    review = reviews[i];
                    break;
                }
            }

            if (review == null)
            {
                return HttpNotFound();
            }

            return View(review);
        }

        // POST: Reviews/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin, Customer")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Score,Reason")] Review review)
        {
            if (ModelState.IsValid)
            {
                db.Entry(review).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(review);
        }

        // GET: Reviews/Delete/5
        /*public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }*/

        // POST: Reviews/Delete/5
        /*[HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]*/
        [HttpPost]
        [Authorize(Roles = "Admin, Customer")]
        public ActionResult DeleteConfirmed(int id)
        {
            Review review = db.Reviews.Find(id);
            db.Reviews.Remove(review);
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
