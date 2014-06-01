/*
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
using System.Linq;
using System.Web;
using System.Web.Mvc;

using SOFA.Infrastructure;
using SOFA.Models;
using SOFA.Models.ViewModels;

namespace SOFA.Controllers
{
    public class TimetableController : DashBoardBaseController
    {
        private DBContext db = new DBContext();

        //
        // GET: /Timetable/
        [Authorize]
        public ActionResult Index()
        {

            var timetables = db.Timetables.ToList().OrderByDescending(t => t.ExpiryDate);
            TimetableIndexViewModel splitNewAndOld = new TimetableIndexViewModel(timetables);
            return View(splitNewAndOld);
        }

        //
        // GET: /Timetable/Details/5
        [Authorize]
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Timetable/Create
        [Authorize(Roles = "Moderator")]
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Timetable/Create
        [HttpPost]
        [Authorize(Roles = "Moderator")]
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
        [Authorize(Roles="Moderator")]
        public ActionResult Build(int id)
        {
            var timetable = db.Timetables.Where(t => t.Id == id).FirstOrDefault();
            return View(timetable);
        }

        //
        // GET: /Timetable/CreateLine
        [Authorize(Roles = "Moderator")]
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
        [Authorize(Roles = "Moderator")]
        public ActionResult CreateLineTime(int id)
        {
            LineTime lt = new LineTime();
            Line l = db.Lines.Where(x => x.Id == id).FirstOrDefault();
            lt.Line = l;
            return PartialView("LineTimeCreate",lt);
        }

        //
        // GET: /Timetable/CreateLine
        [Authorize(Roles = "Moderator")]
        public ActionResult CreateTimetabledClass(int id)
        {
            TimetabledClassCreateEditViewModel tclassmodel = new TimetabledClassCreateEditViewModel();
            TimetabledClass tclass = new TimetabledClass();
            tclassmodel.TimetabledClass = tclass;
            tclassmodel.ClassBases = db.ClassBases;
            Line l = db.Lines.Where(x => x.Id == id).FirstOrDefault();
            tclassmodel.LineID = l.Id;
            return PartialView("TimetabledClassCreate", tclassmodel);
        }

        //
        // GET: /Timetable/EditLineTime
        [Authorize(Roles = "Moderator")]
        public ActionResult EditTime(int id)
        {
            LineTime lt = db.LineTimes.Where(x => x.Id == id).FirstOrDefault();
            lt.StartTimeString = "" + Math.Floor(lt.StartTime) + ":" + (int)Math.Round((lt.StartTime - Math.Floor(lt.StartTime)) * 60);
            lt.EndTimeString = "" + Math.Floor(lt.EndTime) + ":" + (int)Math.Round((lt.EndTime - Math.Floor(lt.EndTime)) * 60);
            return PartialView("LineTimeCreate", lt);
        }

        // GET: /Timetable/EditLineTime
        [Authorize(Roles = "Moderator")]
        [HttpPost]
        public ActionResult EditTime(LineTime lt)
        {
            LineTime toUpdate = db.LineTimes.Where(x => x.Id == lt.Id).FirstOrDefault();
            TimeSpan tmpSpan = TimeSpan.Parse(lt.StartTimeString);
            toUpdate.StartTime = tmpSpan.Hours + (((double)tmpSpan.Minutes / 60d));
            tmpSpan = TimeSpan.Parse(lt.EndTimeString);
            toUpdate.EndTime = tmpSpan.Hours + (((double)tmpSpan.Minutes / 60d));
            toUpdate.Day = lt.Day;
            db.SaveChanges();
            return RedirectToAction("Build", new { id = toUpdate.Line.Timetable.Id });
        }

        //
        // GET: /Timetable/EditLineTime
        [Authorize(Roles = "Moderator")]
        public ActionResult EditTimetabledClass(int id)
        {
            TimetabledClassCreateEditViewModel tclassmodel = new TimetabledClassCreateEditViewModel();
            tclassmodel.TimetabledClass = db.TimetabledClasses.Where(x => x.Id == id).FirstOrDefault();
            tclassmodel.LineID = tclassmodel.TimetabledClass.Line.Id;
            tclassmodel.ClassBases = db.ClassBases;
            return PartialView("TimetabledClassCreate", tclassmodel);
        }

        // GET: /Timetable/EditLineTime
        [Authorize(Roles = "Moderator")]
        [HttpPost]
        public ActionResult EditTimetabledClass(TimetabledClassCreateEditViewModel tclassmodel)
        {
            TimetabledClass toUpdate = db.TimetabledClasses.Where(x => x.Id == tclassmodel.TimetabledClass.Id).FirstOrDefault();
            toUpdate.Capacity = tclassmodel.TimetabledClass.Capacity;
            toUpdate.ClassBaseID = tclassmodel.TimetabledClass.ClassBaseID;
            toUpdate.DisplayName = tclassmodel.TimetabledClass.DisplayName;
            db.SaveChanges();
            return RedirectToAction("Build", new { id = toUpdate.Line.Timetable.Id });
        }

        //
        // GET: /Timetable/EditLineTime
        [Authorize(Roles = "Moderator")]
        public ActionResult DeleteTimetabledClass(int id)
        {
            TimetabledClass tc = db.TimetabledClasses.Where(x => x.Id == id).FirstOrDefault();
            int timetableid = tc.Line.Timetable.Id;
            db.TimetabledClasses.Remove(db.TimetabledClasses.Where(x => x.Id == id).FirstOrDefault());
            db.SaveChanges();
            return RedirectToAction("Build", new { id = timetableid });
        }


        //
        // GET: /Timetable/CreateTimetabledClass
        [Authorize(Roles = "Moderator")]
        [HttpPost]
        public ActionResult CreateTimetabledClass(TimetabledClassCreateEditViewModel tclassmodel)
        {
            var tclass = tclassmodel.TimetabledClass;
            db.TimetabledClasses.Add(tclass);
            Line l = db.Lines.Where(x => x.Id == tclassmodel.LineID).FirstOrDefault();
            l.TimetabledClasses.Add(tclass);
            db.SaveChanges();
            return RedirectToAction("Build", new { id = l.Timetable.Id });
        }

        //
        // GET: /Timetable/CreateLine
        [Authorize(Roles = "Moderator")]
        [HttpPost]
        public ActionResult CreateLineTime(LineTime lt)
        {
            TimeSpan tmpSpan = TimeSpan.Parse(lt.StartTimeString);
            lt.StartTime = tmpSpan.Hours + (((double)tmpSpan.Minutes / 60d));
            tmpSpan = TimeSpan.Parse(lt.EndTimeString);
            lt.EndTime = tmpSpan.Hours + (((double)tmpSpan.Minutes / 60d));
            db.LineTimes.Add(lt);
            Line l = db.Lines.Where(x => x.Id == lt.Id).FirstOrDefault();
            l.LineTimes.Add(lt);
            db.SaveChanges();
            return RedirectToAction("Build", new { id = l.Timetable.Id });
        }


        //
        // GET: /Timetable/DeleteLine/5
        [Authorize(Roles = "Moderator")]
        public ActionResult DeleteLine(int id)
        {
            Line l = db.Lines.Where(x => x.Id == id).FirstOrDefault();
            var sender = l.Timetable.Id;
            db.Lines.Remove(l);
            db.SaveChanges();
            return RedirectToAction("Build", new { id = sender });
        }

        //
        // GET: /Timetable/DeleteLine/5
        [Authorize(Roles = "Moderator")]
        public ActionResult DeleteTime(int id)
        {
            LineTime l = db.LineTimes.Where(x => x.Id == id).FirstOrDefault();
            var sender = l.Line.Timetable.Id;
            db.LineTimes.Remove(l);
            db.SaveChanges();
            return RedirectToAction("Build", new { id = sender });
        }

        [NonAction]
        public override Enum NavProviderTerm()
        {
            return DashboardNavTerms.Timetabling;
        }
    }
}
