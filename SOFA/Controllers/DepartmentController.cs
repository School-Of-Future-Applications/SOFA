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
            return View();
        }

       
        [HttpGet]
        public ActionResult CreateEdit(int? departmentId)
        {
            if (departmentId != null)
                return View(db.Departments.
                                   Where(x => x.id == departmentId).FirstOrDefault());
            return View();
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
                return RedirectToAction("Department", new { departmentId = dep.id });
            }
            else
                return View();
        }

        public ActionResult Delete(int? departmentId)
        {
            Department dep = null;
            try
            {
                if (departmentId == null)
                    throw new Exception();

                dep = db.Departments.Where(x => x.id == departmentId).First();
                dep.Deleted = true;
                db.Entry(dep).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Department", new { departmentId = departmentId });
            }
        }
       
        public ActionResult Department(int? departmentId)
        {
            if (departmentId == null)
                return RedirectToAction("Index");

            Department dep = db.Departments.Where(x => x.id == departmentId)
                            .FirstOrDefault();
            ViewBag.DepartmentId = dep.id;
            return View(dep);
        }

        [ChildActionOnly]
        public PartialViewResult DepartmentSideBar()
        {
            return PartialView(db.Departments.Where(x => x.Deleted == false)
                              .OrderBy(x => x.DepartmentName).ToList());
        }

	}
}