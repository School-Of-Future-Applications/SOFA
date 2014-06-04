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
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

using SOFA.Infrastructure;
using SOFA.Infrastructure.Users;
using SOFA.Models;
using SOFA.Models.ViewModels;

namespace SOFA.Controllers
{
    [Authorize]
    public class LoginController : HttpsBaseController
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            return RedirectPermanent("~/Login/UserLogin");
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult UserLogin(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult UserLogin(UserLoginViewModel login, string returnUrl)
        {
            ClaimsIdentity ident = null;
            SOFAUser user = null;

            if(ModelState.IsValid)
            {
                user = this.UserManager().Find(login.UserName, login.Password);
                if (user == null)
                {
                    ModelState.AddModelError("", "Invalid email or password");
                }
                else if (user.Active && user.EmailConfirmed)
                {
                    ident = this.UserManager().CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                    this.AuthManager().SignOut();
                    this.AuthManager().SignIn(new AuthenticationProperties { IsPersistent = false }, ident);
                    return RedirectToAction("Index", "Dashboard");
                }
                else
                    ModelState.AddModelError("", "Failed to login");
            }
            ViewBag.returnUrl = returnUrl;
            return View(new UserLoginViewModel { UserName = login.UserName });
        }

        public ActionResult UserLogout()
        {
            this.AuthManager().SignOut();
            return RedirectToAction("UserLogin");
        }
	}
}