using SOFA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SOFA.Controllers
{
    public class LineTimeController : Controller
    {

        DBContext db = new DBContext();
        //
        // GET: /LineTime/LineTimeCreate
        public ActionResult LineTimeCreate()
        {
            return View();
        }


        //
        // POST: /Timetable/LineTimeCreate
        [HttpPost]
        public ActionResult LineTimeCreate(LineTime lt)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.LineTimes.Add(lt);
                    db.SaveChanges();
                    return RedirectToAction("LineTimeCreate");
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