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
    public class SectionController : DashBoardBaseController
    {
        //
        // GET: /Section/
        [Authorize(Roles = SOFARole.AUTH_MODERATOR)]
        public ActionResult Index()
        {
            return View(this.DBCon().Sections.ToList());
        }

        //
        // GET: /Section/Create
        public ActionResult Edit(String SectionID = null)
        {
            SectionEditViewModel sevm = new SectionEditViewModel();
            sevm.Section = this.DBCon().Sections.Where(s => s.Id == SectionID).FirstOrDefault();
            var ofids = this.DBCon().SectionFieldOrders.Where(sfo => sfo.Section.Id == sevm.Section.Id).OrderBy(sfo => sfo.Order).Select(x => x.Field.Id).ToList();
            sevm.OrderedFields = new List<Field>();
            foreach(string id in ofids)
            {
                sevm.OrderedFields.Add(this.DBCon().Fields.Where(x => x.Id == id).FirstOrDefault());
            }
            return View(sevm);
        }

        //
        // POST: /Section/Create
        [HttpPost]
        public ActionResult CreateEdit(Section section)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        [Authorize(Roles = SOFARole.AUTH_MODERATOR)]
        public ActionResult CreateSection()
        {
            return PartialView();
        }

        [HttpPost]
        [Authorize(Roles = SOFARole.AUTH_MODERATOR)]
        public ActionResult CreateSection(Section section)
        {
            if (ModelState.IsValid)
            {
                this.DBCon().Sections.Add(section);
                this.DBCon().SaveChanges();
                return RedirectToAction("Index");
            }
            else
                return PartialView();
        }

        [HttpPost]
        [Authorize(Roles = SOFARole.AUTH_MODERATOR)]
        public ActionResult AddField(string sectionId, string type, string prompt)
        {
            Section s = this.DBCon().Sections.Where(x => x.Id == sectionId).FirstOrDefault();
            Field f = new Field(type);
            f.PromptValue = prompt;
            f.Section = s;
            this.DBCon().Fields.Add(f);
            this.DBCon().SaveChanges();
            return Json(f.Id);
        }

        [HttpPost]
        [Authorize(Roles = SOFARole.AUTH_MODERATOR)]
        public ActionResult SetSectionFieldOrder(List<string> FieldIds)
        {
            for(int i = 0;i<FieldIds.Count();i++)
            {
                var fid = FieldIds[i];
                var usfo = this.DBCon().SectionFieldOrders.Where(sfo => sfo.FieldID == fid).FirstOrDefault();
                if(usfo != null)
                {
                    usfo.Order = i;
                }
                else
                {
                    usfo = new SectionFieldOrder();
                    usfo.FieldID = fid;
                    usfo.SectionID = this.DBCon().Fields.Where(f => f.Id == fid).FirstOrDefault().Section.Id;
                    this.DBCon().SectionFieldOrders.Add(usfo);
                }
                this.DBCon().SaveChanges();
            }
            return Json(1);
        }

        //
        // GET: /Section/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Section/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public override Enum NavProviderTerm()
        {
            return DashboardNavTerms.Sections;
        }
    }
}
