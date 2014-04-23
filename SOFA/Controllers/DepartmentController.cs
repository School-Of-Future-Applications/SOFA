using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SOFA.Models;

namespace SOFA.Controllers
{
    public class DepartmentController : Controller
    {
        private DBContext db = new DBContext();
       
        public ActionResult Index()
        {
            ViewBag.NavItem = "Department & Courses";
            return View(db.Departments.ToList());
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