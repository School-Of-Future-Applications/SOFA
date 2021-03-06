﻿/*
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
using SOFA.Models.ViewModels;
using SOFA.Models.ViewModels.FormViewModels;
using System.Web.Routing;

namespace SOFA.Controllers
{
    public class FormController : DashBoardBaseController
    {
        //
        // GET: /Form/
        [Authorize(Roles = SOFARole.AUTH_SOFAADMIN)]
        public ActionResult Index()
        {
            return View(this.DBCon().Forms.ToList());
        }


        [Authorize(Roles = SOFARole.AUTH_SOFAADMIN)]
        public ActionResult Edit(String FormID = null)
        {
            //TODO: Actual logic
            try
            {
                var form = this.DBCon().Forms.Single(f => f.Id == FormID);
                form.FormSections = SOFA.Models.FormSection.Sort(form.FormSections);
                foreach (FormSection fs in form.FormSections)
                {
                    List<Field> fields = fs.Section.Fields.ToList();
                    List<SectionFieldOrder> orders = FieldOrderUtil.
                                                    GetOrderForFields(fields, this.DBCon());
                    try
                    {                        
                        fields = fields.Sort(orders);
                        fs.Section.Fields = fields;
                    }
                    catch
                    {
                        
                    }
                    
                }
                var viewModel = new FormViewModel(form);
                return View(viewModel);
            }
            catch
            {
                return RedirectToAction("Index");
            }
            
            
        }

        [HttpPost]
        [Authorize(Roles = SOFARole.AUTH_SOFAADMIN)]
        public ActionResult Edit(Form form)
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = SOFARole.AUTH_SOFAADMIN)]
        public ActionResult CreateForm()
        {
            return PartialView();
        }

        [HttpPost]
        [Authorize(Roles = SOFARole.AUTH_SOFAADMIN)]
        public ActionResult CreateForm(Form form)
        {
            if(ModelState.IsValid)
            {
                this.DBCon().Forms.Add(form);
                this.DBCon().SaveChanges();
                return RedirectToAction("Index");
            }
            return PartialView();
        }

        //
        // GET: /Form/Delete/5
        [Authorize(Roles = SOFARole.AUTH_SOFAADMIN)]
        public ActionResult Delete(String formId)
        {
            DeleteConfirmationViewModel dcvm = new DeleteConfirmationViewModel()
            {
                DeleteAction = "Delete",
                DeleteController = "Form",
                HeaderText = "Confirm Form Deletion",
                ConfirmationText = "Are you sure you want to delete this Form?",
            };
            dcvm.RouteValues.Add("FormId", formId);
            return PartialView("DeleteConfirmationViewModel", dcvm);
        }

        //
        // POST: /Form/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        [Authorize(Roles = SOFARole.AUTH_SOFAADMIN)]
        public ActionResult DeletePost(String formId)
        {
            try
            {
                Form form = this.DBCon().Forms.Single(f => f.Id == formId);
                this.DBCon().Entry(form).State = EntityState.Deleted;
                this.DBCon().SaveChanges();
            }
            catch { }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize(Roles = SOFARole.AUTH_SOFAADMIN)]
        public JsonResult AddSection(String FormId, String SectionId)
        {
            //TODO
            Form form = this.DBCon().Forms.SingleOrDefault(f => f.Id == FormId);
            if (form == null)
            {
                return Json(new
                {
                    Success = "False",
                    Message = "Could not find form"
                });
            }
            Section section = this.DBCon().Sections.
                                SingleOrDefault(s => s.Id == SectionId);
            if (section == null) 
            {
                return Json(new
                {
                    Success = "False",
                    Message = "Could not find section."
                });
            }
            if (form.FormSections.SingleOrDefault(f => f.SectionId == SectionId)
                        != null)
            {
                return Json(new
                {
                    Success = "False",
                    Message = "You cannot add a section twice to a form."
                });
            }
            Section belowof;
            if (form.FormSections.Count > 0)
            {
                belowof = SOFA.Models.FormSection.Sort(form.FormSections)
                                .ElementAt(form.FormSections.Count - 1).Section;

                if (belowof == null)
                {
                    return Json(new
                    {
                        Success = "False",
                        Message = "Could not find above section."
                    });
                }
            }
            else
            {
                belowof = null;
            }
            FormSection formSection = new FormSection()
            {
                Section = section,
                BelowOf = belowof
            };
            form.FormSections.Add(formSection);
            form.updateModified();
            if (this.TryValidateModel(form))
            {
                this.DBCon().Entry(form).State = System.Data.Entity.EntityState.Modified;
                this.DBCon().SaveChanges();
                return Json(new
                {
                    Success = "True",
                    Message = "Section added to form successfully"
                });
            }
            else return Json(new
            {
                Success = "False",
                Message = "Form failed to validate."
            });
            
            
            
        }

        [Authorize(Roles = SOFARole.AUTH_SOFAADMIN)]
        public PartialViewResult FormSection(String FormId, String SectionId)
        {
            var formsection = this.DBCon().FormSections
                                .SingleOrDefault(f => f.FormId == FormId
                                                    && f.SectionId == SectionId);
            if (formsection == null)
            {
                return null;
            }
            var formSectionVM = new FormSectionViewModel(formsection);
            return PartialView("~/Views/Shared/DisplayTemplates/FormSectionViewModel.cshtml", formSectionVM);
        }

        [Authorize(Roles = SOFARole.AUTH_SOFAADMIN)]
        public ActionResult RemoveSection(String FormId, String SectionId)
        {
            DeleteConfirmationViewModel dcvm = new DeleteConfirmationViewModel()
            {
                DeleteAction = "RemoveSection",
                DeleteController = "Form",
                HeaderText = "Confirm section removal",
                ConfirmationText = "Are you sure you want to remove this section from the form?",
            };
            dcvm.RouteValues.Add("FormId", FormId);
            dcvm.RouteValues.Add("SectionId", SectionId);
            return PartialView("DeleteConfirmationViewModel", dcvm);
        }
        

        //
        // POST: /Form/RemoveSection
        [HttpPost]
        [ActionName("RemoveSection")]
        [Authorize(Roles = SOFARole.AUTH_SOFAADMIN)]
        public ActionResult RemoveSectionPost(String FormId, String SectionId)
        {
            try
            {
                Form form = this.DBCon().Forms.Single(f => f.Id == FormId);

                //Get form sections and sort
                var formSections = SOFA.Models.FormSection.Sort(form.FormSections).ToList();
                var removeFormSection = formSections.Single(fs => fs.SectionId == SectionId);
                var removeIndex = formSections.IndexOf(removeFormSection);
                //Link next formsection belowof to above 
                if (removeIndex != formSections.Count - 1)
                {
                    if (removeIndex == 0)
                    {
                        FormSection newFirstFS = new FormSection()
                        {
                            Section = formSections[removeIndex + 1].Section
                        };
                        formSections[removeIndex + 1] = newFirstFS;                  
                    }
                    else  //Not the last section
                    {
                        formSections[removeIndex + 1].BelowOf = removeFormSection.BelowOf;
                    }         
                }
                //Delete FormSection
                formSections.Remove(removeFormSection);
                form.FormSections = formSections;
                form.updateModified();
                this.DBCon().Forms.Attach(form);
                this.DBCon().Entry(form).State = System.Data.Entity.EntityState.Modified;
                this.DBCon().Entry(removeFormSection).State = EntityState.Deleted;
                this.DBCon().SaveChanges();
                
            }
            catch
            {
            }

            return RedirectToAction("Edit", new { formId = FormId });
        }
        
        [HttpPost]
        [Authorize(Roles = SOFARole.AUTH_SOFAADMIN)]
        public JsonResult UpdateSectionOrder(FormSectionOrderViewModel viewModel)
        {
            
            //Get list of form sections
            Form form = this.DBCon().Forms.
                                SingleOrDefault(f => f.Id == viewModel.FormId);
            
            if (form == null)
            {
                return Json(new
                {
                    Success = "False",
                    Message = "Form was unable to be updated as it could not be found."
                });
            }
            ICollection<FormSection> fsections = form.FormSections;
            //For each ID:
            //  Get formsection where id == section id
            //  Update belowof to be section with ID prev in list
            int count = viewModel.SectionIds.Count();
            for (int i = 0; i < count; i++)
            {
                FormSection fsection = fsections.
                                        SingleOrDefault(f => f.Section.Id == viewModel.SectionIds[i]);
                if (fsection == null)
                {
                    //Can't find form. Bail out.
                    return Json(new
                        {
                            Success = "False",
                            Message = "Form section not found. Section order unable to be updated."
                        });
                }
                if (i == 0) //Top of the list
                {
                    
                    fsection.BelowOf = null;
                }
                else
                {
                    var aboveSectionID = viewModel.SectionIds[i - 1];
                    Section below = this.DBCon().Sections.
                                    SingleOrDefault(s => s.Id == aboveSectionID );
                    fsection.BelowOf = below;
                }                
                this.DBCon().FormSections.Attach(fsection);
                this.DBCon().Entry(fsection).State = System.Data.Entity.EntityState.Modified;

            }

            form.updateModified();
            this.DBCon().SaveChanges();
            return Json(new
            {
                Success = "True",
                Message = "Form Saved."
            });
        }

        public override Enum NavProviderTerm()
        {
            return DashboardNavTerms.Forms;
        }
    }
}