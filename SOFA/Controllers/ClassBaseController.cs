using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SOFA.Models;

namespace SOFA.Controllers
{
    public class ClassBaseController : Controller
    {
        //
        // GET: /ClassBase/
        public ActionResult Index()
        {

            return View();
        }

        //
        // GET: /ClassBase/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /ClassBase/Create
        public ActionResult Create(int courseID)
        {
            return View();
        }

        //
        // POST: /ClassBase/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
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
        // GET: /ClassBase/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /ClassBase/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /ClassBase/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /ClassBase/Delete/5
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
    }
}
