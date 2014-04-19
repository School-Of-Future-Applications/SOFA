using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SOFA.Controllers
{
    public class DepartmentViewController : Controller
    {
        //
        // GET: /DepartmentView/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateEdit(int id = 0)
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateEdit()
        {
            return View();
        }
	}
}