using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SOFA.Models;
using SOFA.Models.ViewModels;

namespace SOFA.Controllers
{
    public class ClassBaseController : Controller
    {
        private DBContext db = new DBContext();
        //
        // GET: /ClassBase/
        public ActionResult Index()
        {

            return View();
        }

        //
        // GET: /ClassBase/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /ClassBase/Create
        public ActionResult Create(int courseID = 1) //default value for debugging only
        {
            var viewModel = new ClassBaseViewModel();
            var course = db.Courses.FirstOrDefault(c => c.Id == courseID);                
            if (course != null)
            {
                viewModel.CourseID = course.Id;
                viewModel.CourseName = course.CourseName;
                return View(viewModel);
            }

            return RedirectToAction("Index");
            
        }

        //
        // POST: /ClassBase/Create
        [HttpPost]
        public ActionResult Create(ClassBaseViewModel viewModel)
        {
            var courses = db.Courses;
            Course course = null;
            if (courses.Count() > 0) 
            {
                course = db.Courses.FirstOrDefault(c => c.Id == viewModel.CourseID);                
            }
            if (course == null) //Course hasn't been created or course list empty. Redirect back to add page.
            {
                ViewBag.ErrorMessage = "Please create a course before adding a Class Base";
                return View(viewModel);
            }            
            if (ModelState.IsValid)
            {
                //Map the view model to the model and add it to the db
                ClassBase classBase = viewModel.ToClassBase(course);
                db.ClassBases.Add(classBase);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(viewModel); //SS validation failed. Try again.
        }

        

        //
        // GET: /ClassBase/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /ClassBase/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /ClassBase/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /ClassBase/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
