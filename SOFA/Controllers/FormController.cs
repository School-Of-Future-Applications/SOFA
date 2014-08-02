using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SOFA.Infrastructure;

namespace SOFA.Controllers
{
    public class FormController : DashBoardBaseController
    {
        //
        // GET: /Form/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateEdit(String FormID = null)
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateEdit(Form form)
        {
            return View();
        }

        //
        // GET: /Form/Delete/5
        public ActionResult Delete(String FormID)
        {
            return View();
        }

        //
        // POST: /Form/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeletePost(String FormID)
        {
            return View();
        }

        public override Enum NavProviderTerm()
        {
            throw new NotImplementedException();
        }
    }
}