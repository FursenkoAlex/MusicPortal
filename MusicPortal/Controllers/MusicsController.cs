using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MusicPortal.Models;

namespace MusicPortal.Controllers
{
    public class MusicsController : Controller
    {
        private MusicPortalContext db = new MusicPortalContext();

        // GET: Musics
        public ActionResult Index()
        {
            return View(db.Musics.ToList());
        }

        // GET: Musics/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Music music = db.Musics.Find(id);
            if (music == null)
            {
                return HttpNotFound();
            }
            return View(music);
        }

        // GET: Musics/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Musics/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Path,Name,Date")] Music music)
        {
            if (ModelState.IsValid)
            {
                db.Musics.Add(music);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(music);
        }

        // GET: Musics/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Music music = db.Musics.Find(id);
            if (music == null)
            {
                return HttpNotFound();
            }
            return View(music);
        }

        // POST: Musics/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Path,Name,Date")] Music music)
        {
            if (ModelState.IsValid)
            {
                db.Entry(music).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(music);
        }

        // GET: Musics/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Music music = db.Musics.Find(id);
            if (music == null)
            {
                return HttpNotFound();
            }
            return View(music);
        }

        // POST: Musics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Music music = db.Musics.Find(id);
            db.Musics.Remove(music);
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
