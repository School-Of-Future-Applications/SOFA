using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using SOFA.Infrastructure;

namespace SOFA.Controllers
{
    public class UserAdminController : DashBoardBaseController
    {
        // GET: /UserAdmin/
        public ActionResult Index()
        {
            return View();
        }
	}
}