using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using SOFA.Infrastructure;
using SOFA.Models;

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
        public ActionResult CreateEdit(String FormID = null)
        {
            //TODO: Actual logic
            Form form = this.DBCon().Forms.FirstOrDefault(); //Editing
            FormSection.Sort(form.FormSections);
            return View(form);
        }

        [HttpPost]
        [Authorize(Roles = SOFARole.AUTH_MODERATOR)]
        public ActionResult CreateEdit(Form form)
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
        public String UpdateSectionOrder(String formID, List<String> SectionIDs)
        {
            //Get list of form sections
            Form form = this.DBCon().Forms.
                                SingleOrDefault(f => f.Id == formID);
            if (form == null)
            {
                return "Fail";
            }
            ICollection<FormSection> fsections = form.FormSections;            
            //For each ID:
            //  Get formsection where id == section id
            //  Update belowof to be section with ID prev in list
            for (int i = 0; i < SectionIDs.Count; i++)
            {
                FormSection fsection = fsections.
                                        SingleOrDefault(f => f.SectionId == SectionIDs[i]);
                if (fsection != null)
                {
                    if (i == 0) //Top of the list
                    {
                        fsection.BelowOf = null;
                    } 
                    else
                    {
                        Section below = this.DBCon().Sections.
                                        SingleOrDefault(s => s.Id == SectionIDs[i - 1]);
                        fsection.BelowOf = below;
                    }
                     
                }
                this.DBCon().FormSections.Attach(fsection);
                this.DBCon().Entry(fsection).State = System.Data.Entity.EntityState.Modified;

            }
            this.DBCon().SaveChanges();
            return "Success";
        }

        public override Enum NavProviderTerm()
        {
            return DashboardNavTerms.Forms;
        }
    }
}