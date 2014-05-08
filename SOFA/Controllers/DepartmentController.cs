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