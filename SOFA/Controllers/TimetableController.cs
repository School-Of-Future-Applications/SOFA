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
            var timetables = db.Timetables.ToList();
            
            return View(timetables.OrderBy(t => t.ExpiryDate));
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
                if (ModelState.IsValid)
                {
                    db.Timetables.Add(t);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View();
     
                
            }
            catch
            {
                return View();
            }
        }

       
    }
}
