using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PPPKBrunoHrgovicMVC.Controllers.FileControllers
{
    public class DirectorFilesController : Controller
    {
        private readonly Model1Container db = new Model1Container();

        ~DirectorFilesController()
        {
            if (db != null)
            {
                db.Dispose();
            }
        }


        // GET: Files
        public ActionResult Index(int id)
        {
            var uploadedFile = db.DirectorUploadedFilesSet.Find(id);
            return File(uploadedFile.Content, uploadedFile.ContentType);
        }

        public ActionResult Delete(int id)
        {
            db.DirectorUploadedFilesSet.Remove(db.DirectorUploadedFilesSet.Find(id));
            db.SaveChanges();
            return Redirect(Request.UrlReferrer.AbsolutePath);
        }
    }
}