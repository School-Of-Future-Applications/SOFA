using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SOFA.Infrastructure;
using SOFA.Models;

namespace SOFA.Controllers
{
    public class FileController : Controller
    {

        [HttpPost]
        public ActionResult UploadFile()
        {
            foreach(String file in Request.Files)
            {
                HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;
                if (hpf.ContentLength == 0)
                    continue;

                return Json(FileManager.SaveFile(hpf));

            }
            return null;
        }

        public FileResult GetFile(string fileId)
        {
            DBContext dbCon = new DBContext();
            var f = dbCon.Files.Where(x=> x.FileID==fileId).FirstOrDefault();
            return File(FileManager.GetFile(fileId), System.Net.Mime.MediaTypeNames.Application.Octet, "SOFADownload"+System.IO.Path.GetExtension(f.Location));
        }

        //
        // GET: /Upload/
        public ActionResult Index()
        {
            return View();
        }
	}
}