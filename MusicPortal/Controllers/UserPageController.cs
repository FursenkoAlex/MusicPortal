using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MusicPortal.Models;

namespace MusicPortal.Controllers
{
    public class UserPageController : Controller
    {
        private MusicPortalContext db = new MusicPortalContext();

        // GET: UserPage
        public ActionResult Index()
        {
            var u = db.Musics.Include(music => music.Ganers);
            return View(u.ToList());
        }

        // GET: UserPage/Create
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.ListGeners= new SelectList(db.Ganers, "Id", "GanreName");
            return View();
        }

        // POST: UserPage/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(HttpPostedFileBase file, Music music)
        {
            if (file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/TrackStore"), fileName);
                var match = Session["userName"].ToString();
                var userName = db.Users.FirstOrDefault(u => u.Login == match);
                var relativePath = "~/TrackStore/" + fileName;

                music.Date = DateTime.Now;
                music.RelativePath = relativePath;
                music.AbsolutePath = path;
                music.FileName = fileName;
                music.User = userName;
                //music.Ganers = db.Ganers.Find(music.Ganers.Id);
                
                file.SaveAs(path);
                db.Musics.Add(music);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public FileResult DownloadTrack(int? id)
        {

            if (id == null)
            {
                ViewBag.Error = "Files not found";
            }

            var item = db.Musics.Find(id);
            var path = item.AbsolutePath;
            var fileName = item.FileName;

            byte[] fileBytes = System.IO.File.ReadAllBytes(path);

            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
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
