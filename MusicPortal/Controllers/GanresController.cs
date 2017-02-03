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
    public class GanresController : Controller
    {
        private MusicPortalContext db = new MusicPortalContext();

        // GET: Ganres
        public ActionResult Index()
        {
            return View(db.Ganers.ToList());
        }

        // GET: Ganres/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ganre ganre = db.Ganers.Find(id);
            if (ganre == null)
            {
                return HttpNotFound();
            }
            return View(ganre);
        }

        // GET: Ganres/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Ganres/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,GanreName")] Ganre ganre)
        {
            if (ModelState.IsValid)
            {
                db.Ganers.Add(ganre);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ganre);
        }

        // GET: Ganres/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ganre ganre = db.Ganers.Find(id);
            if (ganre == null)
            {
                return HttpNotFound();
            }
            return View(ganre);
        }

        // POST: Ganres/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,GanreName")] Ganre ganre)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ganre).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ganre);
        }

        // GET: Ganres/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ganre ganre = db.Ganers.Find(id);
            if (ganre == null)
            {
                return HttpNotFound();
            }
            return View(ganre);
        }

        // POST: Ganres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ganre ganre = db.Ganers.Find(id);
            db.Ganers.Remove(ganre);
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
