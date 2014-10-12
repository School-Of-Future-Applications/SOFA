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
        [Authorize(Roles = SOFARole.AUTH_TEACHER)]
        public ActionResult Index(int classBaseId)
        {
            ClassBase classBase = null;
            try
            {
                classBase = this.DBCon().ClassBases.Where(x => x.Id == classBaseId).First();
            }
            catch
            {
                return new HttpNotFoundResult();
            }
            return View(classBase);
        }

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
            var dcvm = new DeleteConfirmationViewModel()
            {
                DeleteAction = "Delete",
                DeleteController = "ClassBase",
                HeaderText = "Confirm Class Base Deletion",
                ConfirmationText = "Are you sure you want to delete this class base?"
            };
            dcvm.RouteValues.Add("classBaseId", classBaseId);
            return PartialView("DeleteConfirmationViewModel", dcvm);           
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = SOFARole.AUTH_SOFAADMIN)]
        public ActionResult DeletePost(int classBaseId)
        {
            var courseId = this.DBCon().ClassBases.
                Single(cb => cb.Id == classBaseId).
                Course.Id;
            try
            {
                ClassBase cb = this.DBCon().ClassBases.
                                Single(c => c.Id == classBaseId);
                cb.Deleted = true;
                this.DBCon().Entry(cb).State = System.Data.Entity.EntityState.Modified;
                this.DBCon().SaveChanges();

                return RedirectToAction("Index", "Course" ,new { courseId = courseId });
            }
            catch
            {
                return RedirectToAction("Index", "Department");
            }
     
        }

        public JsonResult ClassBaseYearLevels_JSON(int courseId)
        {
            try
            {
                var allClassBases = this.DBCon().Courses.
                                    Single(c => c.Id == courseId).ClassBases.
                                    Where(cb => cb.TimetabledClasses.Count > 0);
                var currentClassBases = allClassBases.Where(cb => cb.TimetabledClasses.
                                            Any(tc => tc.Line.Timetable.ActiveDate < DateTime.Now &&
                                                    tc.Line.Timetable.ExpiryDate > DateTime.Now)).ToList();
                if (currentClassBases.Count == 0)
                {
                    throw new Exception();
                }
                return Json(currentClassBases.Select(cb => cb.YearLevel), JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new
                    {
                        Success = false
                    }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        [Authorize(Roles = SOFARole.AUTH_SOFAADMIN)]
        public ActionResult CreatePrerequisite(int classBaseId)
        {
            CreatePrerequisiteViewModel viewModel = new CreatePrerequisiteViewModel()
            {
                ClassBaseId = classBaseId
            };

            return PartialView(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = SOFARole.AUTH_SOFAADMIN)]
        public ActionResult CreatePrerequisite(CreatePrerequisiteViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var classBase = this.DBCon().ClassBases.
                                        Single(cb => cb.Id == viewModel.ClassBaseId);
                    var section = this.DBCon().Sections.Add(viewModel.Prerequisite);
                    classBase.PreRequisites.Add(section);

                    this.DBCon().Entry(classBase).State = System.Data.Entity.EntityState.Modified;
                    this.DBCon().SaveChanges();
                }
            }               
            catch
            {

            }

            return RedirectToAction("Index", new { classBaseId = viewModel.ClassBaseId });
        }

        [HttpGet]
        [Authorize(Roles = SOFARole.AUTH_SOFAADMIN)]
        public ActionResult AddExistingPrerequisite(int classBaseId)
        {
            var selectableSections = this.DBCon().Sections.ToList();
            var classBasePreReqs = this.DBCon().ClassBases.
                                    Single(cb => cb.Id == classBaseId).
                                    PreRequisites;
            var selectablePreReqs = selectableSections.Except(classBasePreReqs).ToList();
            AddExistingPreReqViewModel viewModel = new AddExistingPreReqViewModel(classBaseId, selectablePreReqs);
            return PartialView("AddExistingPrereq", viewModel);
        }
        
        [HttpPost]
        [Authorize(Roles = SOFARole.AUTH_SOFAADMIN)]
        public ActionResult AddExistingPrerequisite(AddExistingPreReqViewModel viewModel)
        {
            var addedPrereq = this.DBCon().Sections.Single(s => s.Id == viewModel.SelectedSectionId);
            var classBase = this.DBCon().ClassBases.Single(cb => cb.Id == viewModel.ClassBaseId);
            if (!classBase.PreRequisites.Contains(addedPrereq))
            {
                classBase.PreRequisites.Add(addedPrereq);
                this.DBCon().ClassBases.Attach(classBase);
                this.DBCon().Entry(classBase).State = System.Data.Entity.EntityState.Modified;
                this.DBCon().SaveChanges();
            }
            return RedirectToAction("Index", new { classBaseId = viewModel.ClassBaseId });
        }

        [Authorize(Roles = SOFARole.AUTH_SOFAADMIN)]
        public ActionResult RemovePrerequisite(int classBaseId, string sectionId )
        {
            DeleteConfirmationViewModel dcvm = new DeleteConfirmationViewModel()
            {
                DeleteAction = "RemovePrerequisite",
                DeleteController = "ClassBase",
                ConfirmationText = "Are you sure you want to remove this pre-requisite from the class base?",
                HeaderText = "Confirm Pre-requisite Removal"
            };
            try 
	        {   
                var classBase = this.DBCon().ClassBases.
                                    SingleOrDefault(cb => cb.Id == classBaseId);
                var prereq = classBase.PreRequisites.First(pr => pr.Id == sectionId);
                classBase.PreRequisites.Remove(prereq);

                //No exceptions - set route values and continue
                dcvm.RouteValues.Add("classBaseId", classBaseId);
                dcvm.RouteValues.Add("sectionId", sectionId);
	        }
            catch (Exception)
            {
                dcvm.DeleteInvalid = true;
                dcvm.ConfirmationText = "You cannot remove this pre-requisite";
                dcvm.HeaderText = "Error removing pre-requisite";
            }

            return PartialView("DeleteConfirmationViewModel", dcvm);
        }

        [HttpPost]
        [ActionName("RemovePrerequisite")]
        public ActionResult RemovePrerequisitePost(int classBaseId, string sectionId)
        {
            try
            {
                var classBase = this.DBCon().ClassBases.
                                    SingleOrDefault(cb => cb.Id == classBaseId);
                var prereq = classBase.PreRequisites.First(pr => pr.Id == sectionId);
                classBase.PreRequisites.Remove(prereq);

                this.DBCon().ClassBases.Attach(classBase);
                this.DBCon().Entry(classBase).State = System.Data.Entity.EntityState.Modified;
                this.DBCon().SaveChanges();
            }
            catch
            {

            }

            return RedirectToAction("Index", new { classBaseId = classBaseId });
        }

        [NonAction]
        public override Enum NavProviderTerm()
        {
            return DashboardNavTerms.DepartmentCourse;
        }
    }
}
