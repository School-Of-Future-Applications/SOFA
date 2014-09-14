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
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using SOFA.Infrastructure;
using SOFA.Models;
using SOFA.Models.ViewModels.EnrolmentViewModels;
using SOFA.Models.Prefab;

namespace SOFA.Controllers
{
    public class EnrolmentController : HttpsBaseController
    {
        [HttpGet]
        public ActionResult NewEnrolment(string formId)
        {
            Form fromForm = null;
            EnrolmentForm enrolForm = null;
            try
            {
                fromForm = this.DBCon().Forms.Where(x => x.Id == formId).First();
                enrolForm = new EnrolmentForm(fromForm);
                this.DBCon().EnrolmentForms.Add(enrolForm);
                this.DBCon().SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
            }
            catch
            {
                return new HttpNotFoundResult();
            }
            EnrolmentSection firstSection = EnrolmentFormSection.Sort(enrolForm.EnrolmentFormSections).
                                                First().EnrolmentSection;
            return RedirectToAction("Enrol", "Enrolment"
                                   ,new { sectionId = firstSection.Id,
                                          formId = enrolForm.EnrolmentFormId });
        }

        /* Thoms old Enrol method
        [HttpGet]
        public ActionResult Enrol(String enrolmentFormId, string enrolmentSectionId)
        {
            EnrolmentForm eForm = null;
            try
            {
                eForm = this.DBCon().EnrolmentForms.Where(x => x.EnrolmentFormId == enrolmentFormId).First();
            }
            catch
            {
                return new HttpNotFoundResult();
            }
            return View(eForm);
        }

         */

        public ActionResult Enrol(string sectionId, string formId)
        {
            EnrolmentSectionViewModel esvm;
            try
            {
                //Verify section belongs to form
                EnrolmentForm form = this.DBCon().EnrolmentForms
                                        .Single(f => f.EnrolmentFormId == formId);
                EnrolmentFormSection formSection = form.EnrolmentFormSections
                                            .Single(efs => efs.EnrolmentSectionId == sectionId);
                EnrolmentSection section = formSection.EnrolmentSection;
                if (section.OriginalSectionId.Equals(PrefabSection.STUDENT_DETAILS))
                {
                    esvm = new StudentEnrolmentSectionViewModel(section);
                } 
                else
                {
                    esvm = new EnrolmentSectionViewModel(section);

                }
                esvm.SectionNumber = EnrolmentFormSection.Sort(form.EnrolmentFormSections).
                                        ToList().IndexOf(formSection) + 1;
                esvm.TotalSections = form.EnrolmentFormSections.Count;
                esvm.FormId = formId;

                return View(esvm);

            }
            catch
            {
                return new HttpNotFoundResult();
            }
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Enrol(EnrolmentSectionViewModel esvm)
        {
            if (ModelState.IsValid)
            {
                if (esvm.OriginalSectionId.Equals(PrefabSection.STUDENT_DETAILS))
                {
                    SaveStudentDetailsSection(esvm);
                }
                else if (esvm.SectionId.Equals(PrefabSection.COURSE_SELECT))
                {
                    SaveClassSelectSection(esvm);
                }
                else
                {
                    SaveEnrolmentSection(esvm);
                }

                //Get the next section id
                if (esvm.SectionNumber < esvm.TotalSections)
                {
                    try
                    {
                        var formsections = this.DBCon().EnrolmentForms.
                                            Single(f => f.EnrolmentFormId == esvm.FormId).
                                            EnrolmentFormSections;
                        var nextSection = EnrolmentFormSection.Sort(formsections).
                                                ElementAt(esvm.SectionNumber).EnrolmentSection; //SectionNumber is 1-based
                        return RedirectToAction("Enrol", new
                        {
                            sectionId = nextSection.Id,
                            formId = esvm.FormId
                        });
                    }
                    catch
                    {
                        return View(esvm);
                    }
                    
                    
                }
                else
                {
                    //TODO Form Completion
                    return new HttpNotFoundResult();
                }
            }
            
            return View(esvm); 
        }

        private void SaveEnrolmentSection(EnrolmentSectionViewModel esvm)
        {
            EnrolmentSection eSection = esvm.toEnrolmentSection();
            this.DBCon().EnrolmentSections.Attach(eSection);
            this.DBCon().Entry(eSection).State = EntityState.Modified;
            this.DBCon().SaveChanges();
        }

        private void SaveStudentDetailsSection(EnrolmentSectionViewModel esvm)
        {

        }

        private void SaveClassSelectSection(EnrolmentSectionViewModel esvm)
        {

        }

        private void SavePreqSection(EnrolmentSectionViewModel esvm)
        {

        }

        [HttpGet]
        [Authorize(Roles = SOFARole.AUTH_MODERATOR)]
        public ActionResult EnrolmentFormId(string enrolmentFormId)
        {
            EnrolmentForm ef = null;

            try
            {
                ef = this.DBCon().EnrolmentForms
                    .Where(x => x.EnrolmentFormId == enrolmentFormId)
                    .Include(x => x.Student).First();
            }
            catch
            {
                return new HttpNotFoundResult();
            }
            return EnrolmentForm(ef);
        }

        [HttpGet]
        [Authorize(Roles = SOFARole.AUTH_MODERATOR)]
        public ActionResult EnrolmentForm(EnrolmentForm enrolmentForm)
        {
            return PartialView("EnrolmentForm", enrolmentForm);
        }
	}
}