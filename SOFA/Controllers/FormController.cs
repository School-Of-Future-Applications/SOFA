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
    public class FormController : DashBoardBaseController
    {
        //
        // GET: /Form/
        [Authorize(Roles = SOFARole.AUTH_MODERATOR)]
        public ActionResult Index()
        {
            return View(this.DBCon().Forms.ToList());
        }


        [Authorize(Roles = SOFARole.AUTH_MODERATOR)]
        public ActionResult Edit(String FormID = null)
        {
            //TODO: Actual logic
            Form form = this.DBCon().Forms.FirstOrDefault(); //Editing
            FormSection.Sort(form.FormSections);
            return View(form);
        }

        [HttpPost]
        [Authorize(Roles = SOFARole.AUTH_MODERATOR)]
        public ActionResult Edit(Form form)
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = SOFARole.AUTH_MODERATOR)]
        public ActionResult CreateForm()
        {
            return PartialView();
        }

        [HttpPost]
        [Authorize(Roles = SOFARole.AUTH_MODERATOR)]
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
        [Authorize(Roles = SOFARole.AUTH_MODERATOR)]
        public ActionResult Delete(String FormID)
        {
            return View();
        }

        //
        // POST: /Form/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [Authorize(Roles = SOFARole.AUTH_MODERATOR)]
        public ActionResult DeletePost(String FormID)
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = SOFARole.AUTH_MODERATOR)]
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