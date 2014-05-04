using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            return View(db.Departments.OrderBy(x => x.DepartmentName).ToList());
        }

        [HttpGet]
        public PartialViewResult CreateEdit(int? departmentId)
        {
            if (departmentId != null)
                return PartialView(db.Departments.
                                   Where(x => x.id == departmentId).FirstOrDefault());
            return PartialView();
        }

        [HttpPost]
        public ActionResult CreateEdit(Department dep)
        {
            if (ModelState.IsValid)
            {
                if (db.Departments.Any(x => x.id == dep.id))
                {
                    db.Departments.Attach(dep);
                    db.Entry(dep).State = EntityState.Modified;
                }
                else
                    db.Departments.Add(dep);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
                return View();
        }

        public PartialViewResult Department(int departmentId)
        {
            return PartialView(db.Departments.Where(x => x.id == departmentId)
                               .FirstOrDefault());
        }

	}
}