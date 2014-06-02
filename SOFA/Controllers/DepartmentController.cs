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
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using SOFA.Infrastructure;
using SOFA.Models;

namespace SOFA.Controllers
{
    public class DepartmentController : DashBoardBaseController
    {  
        [Authorize]
        public ActionResult Index()
        {
            ViewBag.NavItem = "Department & Courses";
            return View(this.DBCon().Departments.OrderBy(x => x.DepartmentName).ToList());
        }

        [HttpGet]
        [Authorize(Roles = "Moderator")]
        public ActionResult CreateEdit(int? departmentId)
        {
            if (departmentId != null)
                return View(this.DBCon().Departments.
                                   Where(x => x.id == departmentId).FirstOrDefault());
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Moderator")]
        public ActionResult CreateEdit(Department dep)
        {
            if (ModelState.IsValid)
            {
                if (this.DBCon().Departments.Any(x => x.id == dep.id))
                {
                    this.DBCon().Departments.Attach(dep);
                    this.DBCon().Entry(dep).State = EntityState.Modified;
                }
                else
                    this.DBCon().Departments.Add(dep);
                this.DBCon().SaveChanges();
                return RedirectToAction("Department", new { departmentId = dep.id });
            }
            else
                return View();
        }

        [Authorize(Roles = "Moderator")]
        public ActionResult Delete(int? departmentId)
        {
            Department dep = null;
            try
            {
                if (departmentId == null)
                    throw new Exception();

                dep = this.DBCon().Departments.Where(x => x.id == departmentId).First();
                dep.Deleted = true;
                this.DBCon().Entry(dep).State = EntityState.Modified;
                this.DBCon().SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Department", new { departmentId = departmentId });
            }
        }
       
        [Authorize(Roles = "Moderator")]
        public ActionResult Department(int? departmentId)
        {
            if (departmentId == null)
                return RedirectToAction("Index");

            Department dep = this.DBCon().Departments.Where(x => x.id == departmentId)
                            .FirstOrDefault();
            ViewBag.DepartmentId = dep.id;
            return View(dep);
        }

        [NonAction]
        public override Enum NavProviderTerm()
        {
            return DashboardNavTerms.DepartmentCourse;
        }
	}
}