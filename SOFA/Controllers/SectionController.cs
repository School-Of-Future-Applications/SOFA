using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using SOFA.Infrastructure;

namespace SOFA.Controllers
{
    public class SectionController : DashBoardBaseController
    {
        //
        // GET: /Section/
        public ActionResult Index()
        {
            return View();
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
