﻿/*
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
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

using SOFA.Infrastructure;
using SOFA.Models;
using SOFA.Models.ViewModels;
using SOFA.Properties;

namespace SOFA.Controllers
{
    public class UserAdminController : DashBoardBaseController
    {
        // GET: /UserAdmin/
        public ActionResult Index()
        {
            return View(this.DBCon().Persons.Where(person => person.User != null).ToList());
        }

        public async Task<ActionResult> ActiveUser(String userId, bool active = false)
        {
            Person person = null;
            IdentityResult result = null;
            try
            {
                person = this.DBCon().Persons
                             .Where(p => p.User.Id == userId).First();
                if(active)
                {
                    await SendActivationEmail(person);
                }
                else
                {
                    person.User.Active = active;
                    person.User.EmailConfirmed = false;
                    result = this.UserManager().Update(person.User);
                    if (!result.Succeeded)
                        throw new InvalidOperationException();
                }
                return RedirectToAction("UserAdmin", new { personId = person.Id });
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
            SOFAUser newUser = null;
            IdentityResult result = null;

            if(ModelState.IsValid)
            {
                if(PersonController.isPersonForEmail(this, p.Email))
                {
                    ModelState.AddModelError("Email", "Error user already exsists for this email");
                    return View(p);
                }
                newUser = new SOFAUser
                {
                    UserName = p.Email
                   ,Email = p.Email
                };

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

        [HttpGet]
        public ActionResult UserPasswordReset(string userId)
        {
            SOFAUser user = this.UserManager().FindById(userId);
            UserPasswordResetViewModel viewModel = new UserPasswordResetViewModel();

            if (userId == null)
                return RedirectToAction("Index", "Dashboard");
            viewModel.Token = this.UserManager().GeneratePasswordResetToken(user.Id);
            viewModel.UserName = user.UserName;

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult UserPasswordReset(UserPasswordResetViewModel model)
        {
            UserPasswordResetViewModel newModel = new UserPasswordResetViewModel();
            IdentityResult result = null;
            SOFAUser user = null;

            newModel.Token = model.Token;
            newModel.UserName = model.UserName;

            if(ModelState.IsValid)
            {
                user = this.UserManager().Find(model.UserName, model.OldPassword);
                if(user == null)
                {
                    ModelState.AddModelError("OldPassword", "Invalid Old Password");
                    return View(newModel);
                }
                result = this.UserManager().ResetPassword(user.Id, model.Token
                                                         ,model.NewPassword);
                if (result.Succeeded)
                    return RedirectToAction("Index", "Dashboard"); /* FIX */
                IdentityUtil.resultToModelState(ModelState, result);
                return View(newModel);
            }
            return View(newModel);
        }

        [NonAction]
        public async Task SendActivationEmail(Person p)
        {
            String token = this.UserManager().GenerateEmailConfirmationToken(p.User.Id);
            String activationUrl = Url.Action("UserActivation", "UserAdmin"
                                             ,new { userId = p.User.Id
                                                  , token = token }
                                             , protocol: Request.Url.Scheme);

            await this.UserManager().SendEmailAsync(p.User.Id, "SOFA Activation Email"
                ,String.Format(Resources.UserActiviationEmail, p.FullName()
                              ,activationUrl));
        }

        [HttpGet]
        public ActionResult UserActivation(string userId, string token)
        {
            UserActivationViewModel model = new UserActivationViewModel();
            IdentityResult result = null;
            SOFAUser user = this.UserManager().FindById(userId);

            if (user == null)
                return HttpNotFound();
            result = this.UserManager().ConfirmEmail(userId, token);
            if (!result.Succeeded)
                return HttpNotFound();

            model.UserId = userId;
            model.UserName = user.UserName;
            model.Token = token;
            return View(model);
        }

        [HttpPost]
        public ActionResult UserActivation(UserActivationViewModel ua)
        {
            SOFAUser user = null;

            if(ModelState.IsValid)
            {
                user = this.UserManager().FindById(ua.UserId);
                if(user == null)
                {
                    ModelState.AddModelError("", "Invalid User Id");
                    return View(ua);
                }
                user.PasswordHash = this.UserManager().PasswordHasher.HashPassword(ua.Password);
                user.EmailConfirmed = true;
                user.Active = true;
                this.UserManager().Update(user);
                return RedirectToAction("UserLogin", "Login");
            }
            return View(ua);
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