using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace PPPKBrunoHrgovicMVC.Controllers
{
    public class MovieController : Controller
    {
        private readonly Model1Container db = new Model1Container();

        ~MovieController()
        {
            if (db != null)
            {
                db.Dispose();
            }
        }

        // GET: Actor
        public ActionResult Index()
        {
            return View(db.MovieSet);
        }

        // GET: Actor/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            Movie movie = db.MovieSet.Include(a => a.MovieUploadedFiles).SingleOrDefault(i => i.IDMovie == id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // GET: Actor/Create
        public ActionResult Create()
        {
            ViewBag.Directors = db.DirectorSet.ToList();
            ViewBag.Actors = db.ActorSet.ToList();
            return View();
        }

        // POST: Actor/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "Name, Duration, Release, ActorIDActor, DirectorIDDirector")] Movie movie, IEnumerable<HttpPostedFileBase> files)
        {
            movie.MovieUploadedFiles = new List<MovieUploadedFiles>();
            if (ModelState.IsValid)
            {
                foreach (var file in files)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        var picture = new MovieUploadedFiles
                        {
                            Name = System.IO.Path.GetFileName(file.FileName),
                            ContentType = file.ContentType
                        };
                        using (var reader = new System.IO.BinaryReader(file.InputStream))
                        {
                            picture.Content = reader.ReadBytes(file.ContentLength);
                        }
                        movie.MovieUploadedFiles.Add(picture);
                    }
                }
                db.MovieSet.Add(movie);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // GET: Actor/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.Directors = db.DirectorSet.ToList();
            ViewBag.Actors = db.ActorSet.ToList();

            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            Movie movie = db.MovieSet.Include(a => a.MovieUploadedFiles).SingleOrDefault(i => i.IDMovie == id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // POST: Actor/Edit/5
        [HttpPost]
        [ActionName("Edit")]
        public ActionResult Edit(int id, IEnumerable<HttpPostedFileBase> files)
        {
            ViewBag.Directors = db.DirectorSet.ToList();
            ViewBag.Actors = db.ActorSet.ToList();

            Movie movieToUpdate = db.MovieSet.Find(id);

            if (TryUpdateModel(movieToUpdate, "", new string[] { "Name", "Duration", "Release", "ActorIDActor", "DirectorIDDirector"  }))
            {
                if (movieToUpdate.MovieUploadedFiles == null)
                {
                    movieToUpdate.MovieUploadedFiles = new List<MovieUploadedFiles>();
                }
                foreach (var file in files)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        var picture = new MovieUploadedFiles
                        {
                            Name = System.IO.Path.GetFileName(file.FileName),
                            ContentType = file.ContentType
                        };
                        using (var reader = new System.IO.BinaryReader(file.InputStream))
                        {
                            picture.Content = reader.ReadBytes(file.ContentLength);
                        }
                        movieToUpdate.MovieUploadedFiles.Add(picture);
                    }
                }
                db.Entry(movieToUpdate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(movieToUpdate);
        }

        // GET: Actor/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            Movie movie = db.MovieSet.Include(a => a.MovieUploadedFiles).SingleOrDefault(i => i.IDMovie == id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // POST: Actor/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            db.MovieUploadedFilesSet.RemoveRange(db.MovieUploadedFilesSet.Where(f => f.MovieIDMovie == id));
            db.MovieSet.Remove(db.MovieSet.Find(id));
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}