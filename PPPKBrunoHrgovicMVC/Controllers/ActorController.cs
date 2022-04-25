using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace PPPKBrunoHrgovicMVC.Controllers
{
    public class ActorController : Controller
    {
        private readonly Model1Container db = new Model1Container();

        ~ActorController()
        {
            if (db != null)
            {
                db.Dispose();
            }
        }

        // GET: Actor
        public ActionResult Index()
        {
            return View(db.ActorSet);
        }

        // GET: Actor/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            Actor actor = db.ActorSet.Include(a => a.ActorUploadedFiles).SingleOrDefault(i => i.IDActor == id);
            if (actor == null)
            {
                return HttpNotFound();
            }
            return View(actor);
        }

        // GET: Actor/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Actor/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "Name, Age, Gender")] Actor actor, IEnumerable<HttpPostedFileBase> files)
        {
            actor.ActorUploadedFiles = new List<ActorUploadedFiles>();
            if (ModelState.IsValid)
            {
                foreach (var file in files)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        var picture = new ActorUploadedFiles
                        {
                            Name = System.IO.Path.GetFileName(file.FileName),
                            ContentType = file.ContentType
                        };
                        using (var reader = new System.IO.BinaryReader(file.InputStream))
                        {
                            picture.Content = reader.ReadBytes(file.ContentLength);
                        }
                        actor.ActorUploadedFiles.Add(picture);
                    }
                }
                db.ActorSet.Add(actor);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // GET: Apartment/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            Actor actor = db.ActorSet.Include(a => a.ActorUploadedFiles).SingleOrDefault(i => i.IDActor == id);
            if (actor == null)
            {
                return HttpNotFound();
            }
            return View(actor);
        }

        // POST: Apartment/Edit/5
        [HttpPost]
        [ActionName("Edit")]
        public ActionResult Edit(int id, IEnumerable<HttpPostedFileBase> files)
        {
            Actor actorToUpdate = db.ActorSet.Find(id);

            if (TryUpdateModel(actorToUpdate, "", new string[] { "Name", "Age", "Gender" }))
            {
                if (actorToUpdate.ActorUploadedFiles == null)
                {
                    actorToUpdate.ActorUploadedFiles = new List<ActorUploadedFiles>();
                }
                foreach (var file in files)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        var picture = new ActorUploadedFiles
                        {
                            Name = System.IO.Path.GetFileName(file.FileName),
                            ContentType = file.ContentType
                        };
                        using (var reader = new System.IO.BinaryReader(file.InputStream))
                        {
                            picture.Content = reader.ReadBytes(file.ContentLength);
                        }
                        actorToUpdate.ActorUploadedFiles.Add(picture);
                    }
                }
                db.Entry(actorToUpdate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(actorToUpdate);
        }

        // GET: Actor/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            Actor actor = db.ActorSet.Include(a => a.ActorUploadedFiles).SingleOrDefault(i => i.IDActor == id);
            if (actor == null)
            {
                return HttpNotFound();
            }
            return View(actor);
        }

        // POST: Actor/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            db.MovieUploadedFilesSet.RemoveRange(db.MovieUploadedFilesSet.Where(f => f.Movie.ActorIDActor == id));
            db.MovieSet.RemoveRange(db.MovieSet.Where(m => m.ActorIDActor == id));

            db.ActorUploadedFilesSet.RemoveRange(db.ActorUploadedFilesSet.Where(f => f.ActorIDActor == id));
            db.ActorSet.Remove(db.ActorSet.Find(id));
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}