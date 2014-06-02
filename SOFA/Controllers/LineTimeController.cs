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

using SOFA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using SOFA.Infrastructure;

namespace SOFA.Controllers
{
    public class LineTimeController : DashBoardBaseController
    {
        //
        // GET: /LineTime/LineTimeCreate
        [Authorize(Roles = "Moderator")]
        public ActionResult LineTimeCreate()
        {
            return View();
        }


        //
        // POST: /Timetable/LineTimeCreate
        [Authorize(Roles = "Moderator")]
        [HttpPost]
        public ActionResult LineTimeCreate(LineTime lt)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    this.DBCon().LineTimes.Add(lt);
                    this.DBCon().SaveChanges();
                    return RedirectToAction("LineTimeCreate");
                }
                return View();


            }
            catch
            {
                return View();
            }
        }

        [NonAction]
        public override Enum NavProviderTerm()
        {
            return DashboardNavTerms.Timetabling;
        }
	}
}