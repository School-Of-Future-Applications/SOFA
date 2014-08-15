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
        public ActionResult CreateEdit(String SectionID = null)
        {
            return View();
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

        [Authorize(Roles = SOFARole.AUTH_MODERATOR)]
        public ActionResult IndexPartial()
        {
            var sections = this.DBCon().Sections.ToList();
            return View(new SectionSelectViewModel(sections));
        }

        [Authorize(Roles = SOFARole.AUTH_MODERATOR)]
        public ActionResult Section(string SectionId)
        {
            return View();
        }


        public override Enum NavProviderTerm()
        {
            return DashboardNavTerms.Sections;
        }
    }
}
