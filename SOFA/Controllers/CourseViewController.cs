using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SOFA.Models;

namespace SOFA.Controllers
{
    public class CourseViewController : Controller
    {
        private DBContext db = new DBContext();
        //
        // GET: /CourseView/
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /CourseView/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /CourseView/Create
        public ActionResult Create()
        {
            return View();
        }

        
        public JsonResult CourseList(int departmentID)
        {
            var department = db.Departments.First(d => d.id == departmentID);
            return Json(department.Courses);

        }

        
    }
}
