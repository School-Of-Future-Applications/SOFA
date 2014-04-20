using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SOFA.Models;

namespace SOFA.Controllers
{
    public class TimetableController : Controller
    {
        private DBContext db = new DBContext();

        //
        // GET: /Timetable/
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Timetable/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Timetable/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Timetable/Create
        [HttpPost]
        public ActionResult Create(Timetable t)
        {
            try
            {
                // TODO: Add insert logic here
                db.Timetables.Add(t);
     
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

       
    }
}
