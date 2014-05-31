using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using SOFA.Infrastructure;

namespace SOFA.Controllers
{
    public class SettingsController : DashBoardBaseController
    {
        //
        // GET: /Settings/
        public ActionResult Index()
        {
            return View();
        }

        [NonAction]
        public override Enum NavProviderTerm()
        {
            return DashboardNavTerms.SystemConfig;
        }
	}
}