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
            return View(timetables.OrderByDescending(t => t.ExpiryDate));
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

        //
        // GET: /Timetable/Build/5
        public ActionResult Build(int id)
        {
            var timetable = db.Timetables.Where(t => t.Id == id).FirstOrDefault();
            return View(timetable);
        }

        //
        // GET: /Timetable/CreateLine
        public ActionResult CreateLine(int id)
        {
            Line l = new Line();
            db.Timetables.Where(t => t.Id == id).FirstOrDefault().Lines.Add(l);
            l.Timetable = db.Timetables.Where(t => t.Id == id).FirstOrDefault();
            db.Lines.Add(l);
            db.SaveChanges();
            return RedirectToAction("Build", new { id = id });
        }


        //
        // GET: /Timetable/CreateLine
        public ActionResult DeleteLine(int sender, int id)
        {
            Line l = db.Lines.Where(x => x.Id == id).FirstOrDefault();
            db.Lines.Remove(l);
            db.SaveChanges();
            return RedirectToAction("Build", new { id = sender });
        }



       
    }
}
