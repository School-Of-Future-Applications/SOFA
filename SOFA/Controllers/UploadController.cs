using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SOFA.Infrastructure;

namespace SOFA.Controllers
{
    public class UploadController : Controller
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


        //
        // GET: /Upload/
        public ActionResult Index()
        {
            return View();
        }
	}
}