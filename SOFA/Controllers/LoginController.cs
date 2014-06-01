using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using SOFA.Infrastructure;

namespace SOFA.Controllers
{
    public class LoginController : HttpsBaseController
    {
        public ActionResult Index()
        {
            return RedirectPermanent("~/Login/UserLogin");
        }

        public ActionResult UserLogin()
        {
            return View();
        }
	}
}