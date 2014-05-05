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
        public ActionResult CreateEdit(int departmentId, int courseID = 0)
        {
            try
            {
                var department = db.Departments.FirstOrDefault(d => d.id == departmentId);
                if (department != null)
                {
                    CourseCreateViewModel courseViewModel;
                    //Create time
                    if (courseID == 0)
                    {
                        courseViewModel = new CourseCreateViewModel();
                    }
                    else //Edit time
                    {
                        var course = db.Courses.FirstOrDefault(c => c.Id == courseID);
                        courseViewModel = new CourseCreateViewModel(course);
                    }
                    courseViewModel.DepartmentName = department.DepartmentName;
                    courseViewModel.DepartmentId = department.id;

                }
                    return View();
            }
            catch
            {
                return RedirectToAction("Index", "Department");
            }
        }

        //
        //POST: /Course/Create
        [HttpPost]
        public ActionResult CreateEdit(CourseCreateViewModel c)
        {
            return View();
        }

        
    }
}
