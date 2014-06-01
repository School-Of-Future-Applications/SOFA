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
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using SOFA.Infrastructure;
using SOFA.Models;

namespace SOFA.Controllers
{
    public class PersonController : DashboardController
    {
        private DBContext db = new DBContext();

        [HttpGet]
        public ActionResult EditPerson(int? personId)
        {
            Person p = null;

            try
            {
                if(personId == null)
                    throw new InvalidOperationException();
                p = db.Persons.Where(person => person.Id == personId).First();
            }
            catch(InvalidOperationException)
            {
                return RedirectToActionPermanent("Index", "Dashboard");
            }

            return View(p);
        }

        [HttpPost]
        public ActionResult EditPerson(Person p)
        {
            if (ModelState.IsValid)
            {
                if (db.Persons.Any(person => person.Id == p.Id))
                {
                    db.Persons.Attach(p);
                    db.Entry(p).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("UserAdmin", "UserAdmin"
                                           ,new { personId = p.Id });
                }
                else
                    return RedirectToActionPermanent("Index", "Dashboard");
            }
            else
                return View();
        }

        [NonAction]
        public override Enum NavProviderTerm()
        {
            return DashboardNavTerms.None;
        }
	}
}