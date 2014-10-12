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
                //Order fields
                
                try
                {
                    var enrolmentFields = section.EnrolmentFields.ToList();
                    var sectionFieldOrders = FieldOrderUtil.
                                                GetOrderForEnrolmentFields(enrolmentFields, 
                                                                            this.DBCon());

                    section.EnrolmentFields = enrolmentFields.Sort(sectionFieldOrders);
                }
                catch
                {

                }
                
                if (section.OriginalSectionId.Equals(PrefabSection.STUDENT_DETAILS))
                {
                    esvm = new StudentEnrolmentSectionViewModel(section);
                } 
                else if (section.OriginalSectionId.Equals(PrefabSection.COURSE_SELECT))
                {
                    //Get all departments where
                    List<Department> departments = this.DBCon().Departments.
                                        Where(d => !d.Deleted && d.Courses.Any(c => c.ClassBases.Count > 0)).
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
            if (esvm.OriginalSectionId.Equals(PrefabSection.STUDENT_DETAILS))
            {
                if (!SaveStudentDetailsSection(esvm))
                {
                    return View(esvm);
                }
            }
            else if (esvm.OriginalSectionId.Equals(PrefabSection.COURSE_SELECT))
            {
                if (!SaveClassSelectSection(esvm))
                {
                    CourseEnrolmentSectionViewModel cesvm = esvm as CourseEnrolmentSectionViewModel;
                        List<Department> departments = this.DBCon().Departments.
                        Where(d => d.Courses.Any(c => c.ClassBases.Count > 0)).
                        ToList();
                    cesvm.DepartmentSelect = new SelectList(departments, "id", "DepartmentName");
                    cesvm.CourseSelect = new SelectList(Enumerable.Empty<SelectListItem>());
                    cesvm.YearLevelSelect = new SelectList(Enumerable.Empty<SelectListItem>());
                    return View(cesvm);
                }
                else
                {
                    //Check for prereqs
                    CourseEnrolmentSectionViewModel cesvm = esvm as CourseEnrolmentSectionViewModel;
                    var selectedClassabase = this.DBCon().TimetabledClasses.
                                                Single(tc => tc.Id == cesvm.SelectedClassId).ClassBase;
                    if (selectedClassabase.PreRequisites.Count > 0)
                    {
                        return RedirectToAction("RequestPrequisite", new { formId = cesvm.FormId });
                    }
                }
            }
            else
            {
                if(!SaveEnrolmentSection(esvm))
                {
                    return View(esvm);
                }
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
                    return new HttpNotFoundResult();
                }
                    
                    
            }
            else
            {
                var form = this.DBCon().EnrolmentForms.Single(f => f.EnrolmentFormId == esvm.FormId);
                form.Status = Models.EnrolmentForm.EnrolmentStatus.Completed;
                this.DBCon().EnrolmentForms.Attach(form);
                this.DBCon().Entry(form).State = EntityState.Modified;
                this.DBCon().SaveChanges();
                return RedirectToAction("EnrolmentReview", new { formId = esvm.FormId });
            }
            
        }
            
        


        private bool SaveEnrolmentSection(EnrolmentSectionViewModel esvm)
        {
            if (ModelState.IsValid)
            {
                EnrolmentSection eSection = esvm.toEnrolmentSection();

                this.DBCon().EnrolmentSections.Attach(eSection);
                this.DBCon().Entry(eSection).State = EntityState.Modified;
                foreach (var f in eSection.EnrolmentFields)
                {
                    this.DBCon().Entry(f).State = EntityState.Modified;
                }
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
            CourseEnrolmentSectionViewModel cesvm = esvm as CourseEnrolmentSectionViewModel;
            try
            {
                var form = this.DBCon().EnrolmentForms.
                                Single(f => f.EnrolmentFormId == cesvm.FormId);
                var section = form.EnrolmentFormSections.
                                Single(s => s.EnrolmentSection.Id == cesvm.SectionId)
                                .EnrolmentSection;
                var enrolledClass = this.DBCon().TimetabledClasses.
                                Single(c => c.Id == cesvm.SelectedClassId);

                //Check one last time if class is full
                if (this.DBCon().EnrolmentForms.Where(ef => ef.Class.Id == enrolledClass.Id).
                        ToList().Count >= enrolledClass.Capacity)
                {
                    ModelState.AddModelError("SelectedClass", "The class is full, please choose another class");
                    return false;
                }
                form.Class = enrolledClass;
                var field = section.EnrolmentFields.Single(ef => ef.OriginalFieldId == PrefabField.CLASS_SELECT);
                var course = this.DBCon().Courses.Single(c => c.Id == cesvm.SelectedCourse);

                field.Value = String.Format("{0} {1}", course.CourseName, enrolledClass.DisplayName);

                //Save
                this.DBCon().EnrolmentForms.Attach(form);
                this.DBCon().Entry(form).State = EntityState.Modified;
                this.DBCon().SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
            
        }


        [HttpGet]
        public ActionResult RequestPrequisite(string formId)
        {
            try
            {            
                //Get class 
                var form = this.DBCon().EnrolmentForms.
                                        Single(ef => ef.EnrolmentFormId == formId);
                ClassBase cb = form.Class.ClassBase;
                //Double check pre-req is needed
                if (cb == null || cb.PreRequisites.Count == 0)
                    throw new ArgumentOutOfRangeException("No prequisite for this class");
                //Get the needed prereq sections for the class
                var formSections = form.EnrolmentFormSections.ToList();
                PrereqUtil preReqUtil = new PrereqUtil();
                formSections = preReqUtil.RemoveAllPrerequisiteSections(formSections, this.DBCon());
                formSections = preReqUtil.CollectAndAppendPrerequisiteSections(formSections, cb);
                formSections = EnrolmentFormSection.Sort(formSections).ToList();
                //Redirect back to enrolment
                form.EnrolmentFormSections = formSections;
                this.DBCon().Entry(form).State = EntityState.Modified;
                this.DBCon().SaveChanges();

                //Get the next section
                var previousFormSection = formSections.First(efs => efs.EnrolmentSection.OriginalSectionId == PrefabSection.COURSE_SELECT);
                int index = formSections.IndexOf(previousFormSection);
                if (index == formSections.Count - 1)
                    throw new ArgumentOutOfRangeException();
                return RedirectToAction("Enrol", new { sectionId = formSections[index + 1].EnrolmentSection.Id, 
                                            formId = formId });
            }
            catch 
            {
                return new HttpNotFoundResult("No pre-requisite found for this class");
            }
            
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
        public ActionResult EnrolmentReview(string formId)
        {
            try
            {
                var form = this.DBCon().EnrolmentForms.
                                Single(f => f.EnrolmentFormId == formId);
                var sections = EnrolmentFormSection.Sort(form.EnrolmentFormSections).
                                Select(fs => fs.EnrolmentSection);
                //Order fields
                foreach (var sect in sections)
                {
                    List<EnrolmentField> fields = sect.EnrolmentFields.ToList();

                    try
                    {
                        List<SectionFieldOrder> orders = FieldOrderUtil.GetOrderForEnrolmentFields(fields, this.DBCon());
                        fields = fields.Sort(orders);
                        sect.EnrolmentFields = fields;
                    }
                    catch
                    {
                        
                    }
                }
                //Convert to view models
                
                var enrolmentReviewModel = new EnrolmentReviewViewModel()
                {
                    Sections = sections.ToList()
                };

                return View(enrolmentReviewModel);
            }
            catch
            {
                return new HttpNotFoundResult();
            }
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
                        //Check if class full - if so set full flag
                        if (this.DBCon().EnrolmentForms.Where(ef => ef.Class.Id == tc.Id).
                        ToList().Count >= tc.Capacity)
                        {
                            timetableCDM.full = true;
                        }

                        LineClassDisplayModel lcdm = lines.SingleOrDefault(m => m.LineDisplayName == tc.Line.Label);
                        if (lcdm == null)
                        {
                            lcdm = new LineClassDisplayModel()
                            {
                                LineDisplayName = tc.Line.Label,
                                Times = tc.Line.LineTimes.OrderBy(l => l.Day).ToList()
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