using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PPPKBrunoHrgovicMVC.Controllers.FileControllers
{
    public class MovieFilesController : Controller
    {
        private readonly Model1Container db = new Model1Container();

        ~MovieFilesController()
        {
            if (db != null)
            {
                db.Dispose();
            }
        }


        // GET: Files
        public ActionResult Index(int id)
        {
            var uploadedFile = db.MovieUploadedFilesSet.Find(id);
            return File(uploadedFile.Content, uploadedFile.ContentType);
        }

        public ActionResult Delete(int id)
        {
            db.MovieUploadedFilesSet.Remove(db.MovieUploadedFilesSet.Find(id));
            db.SaveChanges();
            return Redirect(Request.UrlReferrer.AbsolutePath);
        }
    }
}