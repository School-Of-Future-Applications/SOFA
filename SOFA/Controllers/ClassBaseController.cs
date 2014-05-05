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
       /* public ActionResult Index(int courseId = 1) //default value for debugging only
        {
            var course = db.Courses.FirstOrDefault(c => c.Id == courseId);
            if (course != null)
            {
                var classBases = db.ClassBases.Where(c => c.Course.Id == course.Id);
                List<ClassBaseViewModel> viewModels = new List<ClassBaseViewModel>();
                foreach (ClassBase c in classBases)
                {
                    viewModels.Add(new ClassBaseViewModel
                    {
                        Id = c.Id,
                        ClassBaseCode = c.ClassBaseCode,
                        YearLevel = c.YearLevel
                    });
                }
                ViewBag.CourseID = course.Id;
                ViewBag.CourseName = course.CourseName;
                return View(viewModels.OrderBy(v => v.YearLevel));
            }

            return RedirectToAction("Index", "Dashboard");
        }*/

        //
        // GET: /ClassBase/Create/5
        public ActionResult Create(int courseId = 1) //default value for debugging only
        {
            ClassBase tmp = new ClassBase();
            tmp.Course = db.Courses.FirstOrDefault(x => x.Id == courseId);
            return View(tmp);
            /*var viewModel = new ClassBaseViewModel();
            var course = db.Courses.FirstOrDefault(c => c.Id == courseId);                
            if (course != null)
            {
                viewModel.CourseID = course.Id;
                viewModel.CourseName = course.CourseName;
                return View(viewModel);
            }

            return RedirectToAction("Index", new { courseID = courseId });*/
        }

        //
        // POST: /ClassBase/Create
        [HttpPost]
        public ActionResult Create(ClassBase classBase)
        {
            if (ModelState.IsValid)
            {
                db.ClassBases.Add(classBase);
                db.SaveChanges();
                return RedirectToAction("Index", "Course", new { courseId = classBase.Course.Id });
            }
            else
                return View();

           /* var courses = db.Courses;
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
            return View(viewModel); //SS validation failed. Try again.*/
        }

        

        //
        // GET: /ClassBase/Edit/5
        public ActionResult Edit(int id)
        {
            ClassBaseViewModel viewModel = new ClassBaseViewModel();
            ClassBase classBase = db.ClassBases.FirstOrDefault(c => c.Id == id);
            if (classBase != null)
            {
                viewModel.Id = classBase.Id;
                viewModel.CourseID = classBase.Course.Id;
                viewModel.CourseName = classBase.Course.CourseName;
                viewModel.ClassBaseCode = classBase.ClassBaseCode;
                viewModel.YearLevel = classBase.YearLevel;

                return View(viewModel);
            }

            return RedirectToAction("Index", new { id = classBase.Course.Id });
        }

        //
        // POST: /ClassBase/Edit
        [HttpPost]
        public ActionResult Edit(ClassBaseViewModel viewModel)
        {
           if (ModelState.IsValid)
           {
               Course course = db.Courses.FirstOrDefault(c => c.Id == viewModel.CourseID);
               ClassBase cb = viewModel.ToClassBase(course);
               db.ClassBases.Attach(cb);
               db.Entry(cb).State = System.Data.Entity.EntityState.Modified;
               db.SaveChanges();

               return RedirectToAction("Index", new { id = viewModel.CourseID });
           }
           else
           {
               return View(viewModel); //Validation Failed. Try again.
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
