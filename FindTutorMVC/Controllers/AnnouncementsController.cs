using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FindTutorMVC.Models;
using Microsoft.AspNet.Identity;

namespace FindTutorMVC.Controllers
{
    public class AnnouncementsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Announcements
        public ActionResult Index()
        {
            List<Announcement> announcements = db.Announcements
                .Include(a => a.Tutor)
                .ToList();
            
            return View(announcements);
        }

        public ActionResult Categories()
        {
            return View();
        }

        public ActionResult AnnouncementsByCategory(string id)
        {
            List<Announcement> announcements = db.Announcements
                .Where(a => a.Category.Equals(id))
                .Include(a => a.Tutor)
                .ToList();

            return View(announcements);
        }

        [Authorize(Roles = "Admin, Customer")]
        public ActionResult MyAnnouncements(string id)
        {
            List<FavouriteAnnouncementToUser> AnnouncementsToUser = db.FavouriteAnnouncementsToUsers
                .Where(e => e.UserId.Equals(id))
                .ToList();

            List<Announcement> announcements = db.Announcements
                .Include(a => a.Tutor)
                .ToList();

            List<Announcement> MyAnnouncements = new List<Announcement>();

            AnnouncementsToUser.ForEach(au => {
                MyAnnouncements.Add(announcements
                    .Find(a => a.Id == au.AnnouncementId));
            });

            return View(MyAnnouncements);
        }

        [Authorize]
        // GET: Announcements/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            List<Announcement> announcements = db.Announcements
                .Include(a => a.Tutor)
                .ToList();

            Announcement announcement = null;

            for (int i = 0; i < announcements.Count; i++)
            {
                if (announcements[i].Id == id)
                {
                    announcement = announcements[i];
                    break;
                }
            }
           
            if (announcement == null)
            {
                return HttpNotFound();
            }

            return View(announcement);
        }

        [Authorize(Roles = "Admin, Tutor")]
        // GET: Announcements/Create
        public ActionResult Create()
        {
            AnnouncementToTutor model = new AnnouncementToTutor();
            model.TutorId = User.Identity.GetUserId();
            model.Announcement = new Announcement();
            model.Announcement.Date = DateTime.Now;
            return View(model);
        }

        // POST: Announcements/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin, Tutor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TutorId, Announcement")] AnnouncementToTutor announcementToTutor)
        {
            if (ModelState.IsValid)
            {
                announcementToTutor.Announcement.Tutor = db.Users.Find(announcementToTutor.TutorId);
                db.Announcements.Add(announcementToTutor.Announcement);
                db.SaveChanges();
                
                return RedirectToAction("Index");
            }

            return View(announcementToTutor);
        }

        [Authorize(Roles = "Admin, Customer")]
        public ActionResult AddAnnouncementToFavourites(string id, int announcement)
        {
            FavouriteAnnouncementToUser model = new FavouriteAnnouncementToUser();
            model.UserId = id;
            model.AnnouncementId = announcement;
            db.FavouriteAnnouncementsToUsers.Add(model);
            db.SaveChanges();

            return RedirectToAction("MyAnnouncements/" + id);
        }

        [Authorize(Roles = "Admin, Customer")]
        public ActionResult RemoveAnnouncementFromFavourites(string id, int announcement)
        {
            FavouriteAnnouncementToUser model = db.FavouriteAnnouncementsToUsers
                .Single(a => a.AnnouncementId == announcement && a.UserId.Equals(id));

            db.FavouriteAnnouncementsToUsers.Remove(model);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin, Tutor")]
        // GET: Announcements/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            List<Announcement> announcements = db.Announcements
                .Include(a => a.Tutor)
                .ToList();

            Announcement announcement = null;

            for (int i = 0; i < announcements.Count; i++)
            {
                if (announcements[i].Id == id)
                {
                    announcement = announcements[i];
                    break;
                }
            }

            if (announcement == null)
            {
                return HttpNotFound();
            }

            return View(announcement);
        }

        // POST: Announcements/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin, Tutor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Category,Price,Difficulty,Description,Score,Date")] Announcement announcement)
        {
            if (ModelState.IsValid)
            {
                db.Entry(announcement).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(announcement);
        }

        // GET: Announcements/Delete/5
        /*public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Announcement announcement = db.Announcements.Find(id);
            if (announcement == null)
            {
                return HttpNotFound();
            }
            return View(announcement);
        }*/

        // POST: Announcements/Delete/5
        /*[HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]*/
        [Authorize(Roles = "Admin, Tutor")]
        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            Announcement announcement = db.Announcements.Find(id);
            db.Announcements.Remove(announcement);
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
