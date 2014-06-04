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
        //
        // GET: /ClassBase/Create/5
        [Authorize(Roles = SOFARole.AUTH_MODERATOR)]
        public ActionResult CreateEdit(int courseId = 0, int classBaseId = 0) //default value for debugging only
        {
            ClassBaseViewModel viewModel = null;
            try
            {
                if (classBaseId == 0)
                {
                    var course = this.DBCon().Courses.First(c => c.Id == courseId);
                    viewModel = new ClassBaseViewModel(course);
                }
                else
                {
                    var classBase = this.DBCon().ClassBases.First(c => c.Id == classBaseId);
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
        [Authorize(Roles = SOFARole.AUTH_MODERATOR)]
        public ActionResult CreateEdit(ClassBaseViewModel viewModel)
        {
            ClassBase classBase = null;
            try
            {
                if (!ModelState.IsValid)
                    return View(viewModel);
                if(viewModel.Id == 0)
                {
                    var course = this.DBCon().Courses.First(c => c.Id == viewModel.CourseID);
                    classBase = new ClassBase();
                    classBase.ClassBaseCode = viewModel.ClassBaseCode;
                    classBase.YearLevel = viewModel.YearLevel;
                    classBase.Course = course;
                    this.DBCon().ClassBases.Add(classBase);
                }
                else
                {
                    classBase = this.DBCon().ClassBases.First(cb => cb.Id == viewModel.Id);
                    classBase.ClassBaseCode = viewModel.ClassBaseCode;
                    classBase.YearLevel = viewModel.YearLevel;
                    this.DBCon().ClassBases.Attach(classBase);
                    this.DBCon().Entry(classBase).State = System.Data.Entity.EntityState.Modified;    
                }
                this.DBCon().SaveChanges();
                return RedirectToAction("Index", "Course", new { courseId = classBase.Course.Id });
            }
            catch
            {
                return RedirectToAction("Index", "Course", new { courseId = viewModel.CourseID });
            }
        }

        //
        // GET: /ClassBase/Delete/5
        [Authorize(Roles = SOFARole.AUTH_SOFAADMIN)]
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
