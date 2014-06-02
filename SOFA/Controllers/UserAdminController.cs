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

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using SOFA.Infrastructure;
using SOFA.Models;

namespace SOFA.Controllers
{
    public class UserAdminController : DashBoardBaseController
    {
        // GET: /UserAdmin/
        public ActionResult Index()
        {
            return View(this.DBCon().Persons.Where(person => person.User != null).ToList());
        }

        public ActionResult LockUser(String userId, bool userLock = true)
        {
            IdentityResult result = null;
            IdentityUser user = this.UserManager().FindById(userId);
            Person userPerson = null;

            try
            {
                if (user == null)
                    throw new InvalidOperationException();

                user.LockoutEnabled = userLock;
                result = this.UserManager().Update(user);
                if (result.Succeeded)
                {
                    userPerson = this.DBCon().Persons
                                .Where(person => person.User.Id == userId)
                                .First();
                    return RedirectToAction("UserAdmin", new { personId = userPerson.Id });
                }
                throw new InvalidOperationException(); 
            }
            catch(InvalidOperationException)
            {
                return RedirectToActionPermanent("Index", "Dashboard");   
            }
        }

        [HttpGet]
        public ActionResult NewUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NewUser(Person p)
        {
            IdentityUser newUser = null;
            IdentityResult result = null;

            if(ModelState.IsValid)
            {
                if(PersonController.isPersonForEmail(this, p.Email))
                {
                    ModelState.AddModelError("Email", "Error user already exsists for this email");
                    return View(p);
                }
                newUser = new IdentityUser { UserName = p.Email, Email = p.Email
                                           , LockoutEnabled = true};
                result = this.UserManager().Create(newUser, "rgererggrerergger");
                if(!result.Succeeded)
                {
                    IdentityUtil.resultToModelState(ModelState, result);
                    return View(p);
                }

                p.User = newUser;
                PersonController.UpdatePerson(this, p);
                return RedirectToAction("UserAdmin", new { personId = p.Id });
            }
            return View(p);
        }

        public ActionResult UserAdmin(int personId)
        {
            Person p = null;

            try
            {
                p = this.DBCon().Persons.Where(person => person.Id == personId).First();
            }
            catch(InvalidOperationException)
            {
                return RedirectToActionPermanent("Index", "Dashboard");
            }
           
            return View(p);
        }

        [NonAction]
        public override Enum NavProviderTerm()
        {
            return DashboardNavTerms.UserAdmin;
        }
	}
}