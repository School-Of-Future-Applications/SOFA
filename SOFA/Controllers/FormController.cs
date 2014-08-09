using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using SOFA.Infrastructure;
using SOFA.Models;

namespace SOFA.Controllers
{
    public class FormController : DashBoardBaseController
    {
        //
        // GET: /Form/
        [Authorize(Roles = SOFARole.AUTH_MODERATOR)]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = SOFARole.AUTH_MODERATOR)]
        public ActionResult CreateEdit(String FormID = null)
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = SOFARole.AUTH_MODERATOR)]
        public ActionResult CreateEdit(Form form)
        {
            return View();
        }

        //
        // GET: /Form/Delete/5
        [Authorize(Roles = SOFARole.AUTH_MODERATOR)]
        public ActionResult Delete(String FormID)
        {
            return View();
        }

        //
        // POST: /Form/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [Authorize(Roles = SOFARole.AUTH_MODERATOR)]
        public ActionResult DeletePost(String FormID)
        {
            return View();
        }

        public override Enum NavProviderTerm()
        {
            return DashboardNavTerms.Forms;
        }
    }
}