using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SOFA.Models;

namespace SOFA.Controllers
{
    public class TimetabledClassController : Controller
    {
        private DBContext db = new DBContext();
        //
        // GET: /TimetabledClass/
        public ActionResult Index(int baseClassID = 1) //default value for debugging purposes only
        {
            List<String> timetableNames = new List<String>
            {
                "All",
                "Unassigned"
            };
            /* Append all the timetable names to the list */
            var timetables = db.Timetables.ToList();
            timetables.ForEach(t => timetableNames.Add(t.TimetableIdentifier));

            return View();
        }


	}
}