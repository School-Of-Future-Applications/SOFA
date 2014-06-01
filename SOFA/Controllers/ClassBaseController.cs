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
    
    public class ClassBaseController : DashBoardBaseController
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
        public ActionResult CreateEdit(int courseId = 0, int classBaseId = 0) //default value for debugging only
        {
            ClassBaseViewModel viewModel = null;
            try
            {
                if (classBaseId == 0)
                {
                    var course = db.Courses.First(c => c.Id == courseId);
                    viewModel = new ClassBaseViewModel(course);
                }
                else
                {
                    var classBase = db.ClassBases.First(c => c.Id == classBaseId);
                    viewModel = new ClassBaseViewModel(classBase);
                }
                return View(viewModel);
            }
            catch
            {
                return RedirectToAction("Index", "Course", new { courseId = courseId });
            }
        }

        //
        // POST: /ClassBase/Create
        [HttpPost]
        public ActionResult CreateEdit(ClassBaseViewModel viewModel)
        {
            ClassBase classBase = null;
            try
            {
                if (!ModelState.IsValid)
                    return View(viewModel);
                if(viewModel.Id == 0)
                {
                    var course = db.Courses.First(c => c.Id == viewModel.CourseID);
                    classBase = new ClassBase();
                    classBase.ClassBaseCode = viewModel.ClassBaseCode;
                    classBase.YearLevel = viewModel.YearLevel;
                    classBase.Course = course;
                    db.ClassBases.Add(classBase);
                }
                else
                {
                    classBase = db.ClassBases.First(cb => cb.Id == viewModel.Id);
                    classBase.ClassBaseCode = viewModel.ClassBaseCode;
                    classBase.YearLevel = viewModel.YearLevel;
                    db.ClassBases.Attach(classBase);
                    db.Entry(classBase).State = System.Data.Entity.EntityState.Modified;    
                }
                db.SaveChanges();
                return RedirectToAction("Index", "Course", new { courseId = classBase.Course.Id });
            }
            catch
            {
                return RedirectToAction("Index", "Course", new { courseId = viewModel.CourseID });
            }
        }

        

        //
        // GET: /ClassBase/Delete/5
        public ActionResult Delete(int classBaseId)
        {
            return View();
        }

        [NonAction]
        public override Enum NavProviderTerm()
        {
            return DashboardNavTerms.DepartmentCourse;
        }
    }
}
