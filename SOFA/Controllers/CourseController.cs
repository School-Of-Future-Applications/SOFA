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

        public ActionResult Index(int courseId)
        {
            return View(db.Courses.FirstOrDefault(x => x.Id == courseId));
        }

        //
        // GET: /Course/Create
        public ActionResult CreateEdit(int departmentId = 0, int courseId = 0)
        {
            try
            {
                var department = db.Departments.First(d => d.id == departmentId);

                CourseCreateViewModel courseViewModel;
                //Create time
                if (courseId == 0)
                    courseViewModel = new CourseCreateViewModel();
                else //Edit time
                {
                    var course = db.Courses.First(c => c.Id == courseId);
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
            Course course = null;
            try
            {
                if (ModelState.IsValid)
                {
                    //Adding
                    if (c.ID == 0)
                    {
                        Department department = db.Departments.First(d => d.id == c.DepartmentId);
                        course = new Course();
                        course.Department = department;
                        course.CourseName = c.CourseName;
                        course.CourseCode = c.CourseCode;
                        db.Courses.Add(course);
                        db.SaveChanges();
                    }
                    else //Editing
                    {
                        course = db.Courses.First(dbCourse => dbCourse.Id == c.ID);
                        course.CourseName = c.CourseName;
                        course.CourseCode = c.CourseCode;
                        db.Courses.Attach(course);
                        db.Entry(course).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }

                    return RedirectToAction("Department", "Department"
                                           ,new { departmentId = course.Department.id });

                }
                return View(c);
            }
            catch
            {
                return RedirectToAction("Department", "Department"
                                       ,new { departmentId = c.DepartmentId });
            }
        }
    }
}
