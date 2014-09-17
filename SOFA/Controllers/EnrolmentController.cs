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
                else if (section.OriginalSectionId.Equals(PrefabSection.COURSE_SELECT))
                {
                    //Get all departments where
                    List<Department> departments = this.DBCon().Departments.
                                        Where(d => d.Courses.Any(c => c.ClassBases.Count > 0)).
                                        ToList();
                    esvm = new CourseEnrolmentSectionViewModel(section, departments);
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
            bool saveSuccessful;
            if (esvm.OriginalSectionId.Equals(PrefabSection.STUDENT_DETAILS))
            {
                saveSuccessful = SaveStudentDetailsSection(esvm);
            }
            else if (esvm.SectionId.Equals(PrefabSection.COURSE_SELECT))
            {
                saveSuccessful = SaveClassSelectSection(esvm);
            }
            else
            {
                saveSuccessful = SaveEnrolmentSection(esvm);
            }

            //Save not successful - send model back to view
            if (!saveSuccessful)
                return View(esvm);

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
                    return new HttpNotFoundResult();
                }
                    
                    
            }
            else
            {
                //TODO Form Completion
                return new HttpNotFoundResult();
            }
            
        }
            
        


        private bool SaveEnrolmentSection(EnrolmentSectionViewModel esvm)
        {
            if (ModelState.IsValid)
            {
                EnrolmentSection eSection = esvm.toEnrolmentSection();
                this.DBCon().EnrolmentSections.Attach(eSection);
                this.DBCon().Entry(eSection).State = EntityState.Modified;
                this.DBCon().SaveChanges();

                return true;
            }

            return false;
            
        }

        private bool SaveStudentDetailsSection(EnrolmentSectionViewModel esvm)
        {
            StudentEnrolmentSectionViewModel studentVM = esvm as StudentEnrolmentSectionViewModel;
            var student = studentVM.Student;          
            try
            {
                var section = this.DBCon().EnrolmentSections.
                                Single(s => s.Id == studentVM.SectionId);
                //Transfer student details to fields
                section.EnrolmentFields.
                    Single(e => e.OriginalFieldId.Equals(PrefabField.FIRSTNAME)).
                    Value = student.GivenNames;
                section.EnrolmentFields.
                    Single(e => e.OriginalFieldId == PrefabField.LASTNAME).
                    Value = student.LastName;
                section.EnrolmentFields.
                    Single(e => e.OriginalFieldId == PrefabField.STUDENT_EMAIL).
                    Value = student.Email;
                section.EnrolmentFields.
                    Single(e => e.OriginalFieldId == PrefabField.MOBILE_NUMBER).
                    Value = student.MobileNumber;
                section.EnrolmentFields.
                    Single(e => e.OriginalFieldId == PrefabField.PHONE_NUMBER).
                    Value = student.PhoneNumber;
                this.DBCon().EnrolmentSections.Attach(section);
                this.DBCon().Entry(section).State = EntityState.Modified;
                this.DBCon().SaveChanges();

                //Attach student to form
                var form = this.DBCon().EnrolmentForms.
                            Single(f => f.EnrolmentFormId == studentVM.FormId);
                this.DBCon().Students.Add(student);
                form.Student = student;
                this.DBCon().EnrolmentForms.Attach(form);
                this.DBCon().Entry(form).State = EntityState.Modified;
                this.DBCon().SaveChanges();
           }
           catch
           {
                return false;
           }

            return true;

        }

        private bool SaveClassSelectSection(EnrolmentSectionViewModel esvm)
        {
            return true;
        }

        private bool SavePreqSection(EnrolmentSectionViewModel esvm)
        {
            return true;
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

        [HttpGet]
        public ActionResult TimetableDisplay(int courseId, string yearLevel)
        {
            List<LineClassDisplayModel> lines = new List<LineClassDisplayModel>();
            try
            {
                var course = this.DBCon().Courses.Single(c => c.Id == courseId);
                var classbases = course.ClassBases.Where(cb => cb.YearLevel == yearLevel);
                var classes = classbases.Select(cb => cb.TimetabledClasses);    
                foreach (var tcList in classes)
                {
                    foreach (var tc in tcList)
                    {
                        TimetabledClassDisplayModel timetableCDM = new TimetabledClassDisplayModel(tc);
                        LineClassDisplayModel lcdm = lines.SingleOrDefault(m => m.LineDisplayName == tc.Line.Label);
                        if (lcdm == null)
                        {
                            lcdm = new LineClassDisplayModel()
                            {
                                LineDisplayName = tc.Line.Label,
                                Times = tc.Line.LineTimes.ToList()
                            };
                            lines.Add(lcdm);
                        }
                        lcdm.Classes.Add(timetableCDM);
                       
                    }
                }
            }
            catch
            {
                return null;
            }
            ClassSelectViewModel csvm = new ClassSelectViewModel()
            {
                Lines = lines
            };
            return PartialView("LineClassDisplayModel", csvm);
        }
        
        
	}

    
}