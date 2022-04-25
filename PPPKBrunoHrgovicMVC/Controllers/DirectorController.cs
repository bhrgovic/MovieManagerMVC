using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace PPPKBrunoHrgovicMVC.Controllers
{
    public class DirectorController : Controller
    {
        private readonly Model1Container db = new Model1Container();

        ~DirectorController()
        {
            if (db != null)
            {
                db.Dispose();
            }
        }

        // GET: Actor
        public ActionResult Index()
        {
            return View(db.DirectorSet);
        }

        // GET: Actor/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            Director director = db.DirectorSet.Include(a => a.DirectorUploadedFiles).SingleOrDefault(i => i.IDDirector == id);
            if (director == null)
            {
                return HttpNotFound();
            }
            return View(director);
        }

        // GET: Actor/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Actor/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "Name, Age, Gender")] Director director, IEnumerable<HttpPostedFileBase> files)
        {
            director.DirectorUploadedFiles = new List<DirectorUploadedFiles>();
            if (ModelState.IsValid)
            {
                foreach (var file in files)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        var picture = new DirectorUploadedFiles
                        {
                            Name = System.IO.Path.GetFileName(file.FileName),
                            ContentType = file.ContentType
                        };
                        using (var reader = new System.IO.BinaryReader(file.InputStream))
                        {
                            picture.Content = reader.ReadBytes(file.ContentLength);
                        }
                        director.DirectorUploadedFiles.Add(picture);
                    }
                }
                db.DirectorSet.Add(director);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            Director director = db.DirectorSet.Include(a => a.DirectorUploadedFiles).SingleOrDefault(i => i.IDDirector == id);
            if (director == null)
            {
                return HttpNotFound();
            }
            return View(director);
        }


        [HttpPost]
        [ActionName("Edit")]
        public ActionResult Edit(int id, IEnumerable<HttpPostedFileBase> files)
        {
            Director directorToUpdate = db.DirectorSet.Find(id);

            if (TryUpdateModel(directorToUpdate, "", new string[] { "Name", "Age", "Gender" }))
            {
                if (directorToUpdate.DirectorUploadedFiles == null)
                {
                    directorToUpdate.DirectorUploadedFiles = new List<DirectorUploadedFiles>();
                }
                foreach (var file in files)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        var picture = new DirectorUploadedFiles
                        {
                            Name = System.IO.Path.GetFileName(file.FileName),
                            ContentType = file.ContentType
                        };
                        using (var reader = new System.IO.BinaryReader(file.InputStream))
                        {
                            picture.Content = reader.ReadBytes(file.ContentLength);
                        }
                        directorToUpdate.DirectorUploadedFiles.Add(picture);
                    }
                }
                db.Entry(directorToUpdate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(directorToUpdate);
        }

        // GET: Actor/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            Director director = db.DirectorSet.Include(a => a.DirectorUploadedFiles).SingleOrDefault(i => i.IDDirector == id);
            if (director == null)
            {
                return HttpNotFound();
            }
            return View(director);
        }

        // POST: Actor/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            db.MovieUploadedFilesSet.RemoveRange(db.MovieUploadedFilesSet.Where(f => f.Movie.DirectorIDDirector == id));
            db.MovieSet.RemoveRange(db.MovieSet.Where(m => m.DirectorIDDirector == id));

            db.DirectorUploadedFilesSet.RemoveRange(db.DirectorUploadedFilesSet.Where(f => f.DirectorIDDirector == id));
            db.DirectorSet.Remove(db.DirectorSet.Find(id));
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}