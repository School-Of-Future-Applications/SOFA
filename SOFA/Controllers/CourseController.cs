/*
 *  School Of Future Applications
 *
 *  Copyright (C) 2014  Terminal Coding
 *
 * This program is free software; you can redistribute it and/or
 * modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation; either version 2
 * of the License, or (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using SOFA.Infrastructure;
using SOFA.Models;
using SOFA.Models.ViewModels;


namespace SOFA.Controllers
{
    public class CourseController : DashBoardBaseController
    {
        private DBContext db = new DBContext();

        [Authorize]
        public ActionResult Index(int courseId = 0)
        {
            try
            {
                return View(db.Courses.First(x => x.Id == courseId));
            }
            catch
            {
                return RedirectToAction("Index", "Department");
            }
        }

        //
        // GET: /Course/Create
        [Authorize(Roles = "Moderator")]
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
        [Authorize(Roles = "Moderator")]
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

        [NonAction]
        public override Enum NavProviderTerm()
        {
            return DashboardNavTerms.DepartmentCourse;
        }
    }
}
