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
    public class TimetabledClassController : DashBoardBaseController
    {
        [Authorize(Roles = SOFARole.AUTH_TEACHER)]
        public ActionResult TimetabledClass(int timetabledClassId)
        {
            TimetabledClass tc = null;

            try
            {
                tc = this.DBCon().TimetabledClasses.Where(x => x.Id == timetabledClassId).First();
            }
            catch
            {
                return new HttpNotFoundResult();
            }
            return View(tc);
        }

        [Authorize(Roles = SOFARole.AUTH_SOFAADMIN)]
        public ActionResult TimetabledClassEnrolmentForm(string enrolmentFormId)
        {
            EnrolmentForm ef = null;

            try
            {
                ef = this.DBCon().EnrolmentForms.Where(x => x.EnrolmentFormId == enrolmentFormId).First();
            }
            catch
            {
                return new HttpNotFoundResult();
            }
            return View(ef);
        }

        [NonAction]
        public override Enum NavProviderTerm()
        {
            return DashboardNavTerms.Timetabling;
        }

        [HttpGet]
        [Authorize(Roles = SOFARole.AUTH_MODERATOR)]
        public ActionResult StudentMove(List<String> Ids)
        {
            //Error checking
            if (Ids.Count == 0)
                throw new ArgumentOutOfRangeException("List parameter cannot be empty");
            //Get current timetabledClassId
            String firstId = Ids[0];
            var tc = this.DBCon().EnrolmentForms.
                                    Single(ef => ef.EnrolmentFormId == firstId)
                                    .Class;
            //Get all timetabled  in course classes except current
            var course = tc.ClassBase.Course;
            List<TimetabledClass> availableClasses = course.ClassBases.
                                                        SelectMany(cb => cb.TimetabledClasses).
                                                        Where(t => t.Id != tc.Id).ToList();
            //Create view model
            StudentMoveViewModel viewModel = new StudentMoveViewModel(availableClasses);
            viewModel.EnrolmentFormIds = Ids;
            viewModel.CurrentTimetabledClassId = tc.Id;
            return PartialView(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = SOFARole.AUTH_MODERATOR)]
        public ActionResult StudentMove(StudentMoveViewModel viewModel)
        {
            try
            {

            }
            catch
            {

            }
            return RedirectToAction("TimetabledClass",
                    new { timetabledClassId = viewModel.CurrentTimetabledClassId });    
        }
	}
}