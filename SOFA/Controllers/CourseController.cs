using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SOFA.Models;
using SOFA.Models.ViewModels;


namespace SOFA.Controllers
{
    public class CourseController : Controller
    {
        private DBContext db = new DBContext();

        //
        // GET: /Course/Create
        public ActionResult CreateEdit(int departmentId = 1, int courseID = 0)
        {
            try
            {
                var department = db.Departments.First(d => d.id == departmentId);

                CourseCreateViewModel courseViewModel;
                //Create time
                if (courseID == 0)
                {
                    courseViewModel = new CourseCreateViewModel();
                }
                else //Edit time
                {
                    var course = db.Courses.First(c => c.Id == courseID);
                    courseViewModel = new CourseCreateViewModel(course);
                }
                courseViewModel.DepartmentName = department.DepartmentName;
                courseViewModel.DepartmentId = department.id;

                return View(courseViewModel);
            }
            catch
            {
                //Department or course probably doesn't exist
                return RedirectToAction("Index", "Department");
            }
        }

        //
        //POST: /Course/Create
        [HttpPost]
        public ActionResult CreateEdit(CourseCreateViewModel c)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //Adding
                    if (c.ID == 0)
                    {
                        Department department = db.Departments.First(d => d.id == c.DepartmentId);
                        Course course = new Course();
                        course.Department = department;
                        course.CourseName = c.CourseName;
                        course.CourseCode = c.CourseCode;
                        db.Courses.Add(course);
                        db.SaveChanges();
                    }
                    else //Editing
                    {
                        Course course = db.Courses.First(dbCourse => dbCourse.Id == c.ID);
                        course.CourseName = c.CourseName;
                        course.CourseCode = c.CourseCode;
                        db.Courses.Attach(course);
                        db.Entry(course).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                    
                    return RedirectToAction("Index", "Department");

                }

                return View(c);
            }
            catch
            {
                return RedirectToAction("Index", "Department");
            }
            
            
        }

        
    }
}
