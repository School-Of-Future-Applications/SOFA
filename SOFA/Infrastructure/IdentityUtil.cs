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
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

using SOFA.Infrastructure.Users;
using SOFA.Models;

namespace SOFA.Infrastructure
{
    public static class IdentityUtil
    {
        public static IAuthenticationManager AuthManager(this Controller @this)
        {
            return @this.HttpContext.GetOwinContext().Authentication;
        }

        public static SOFAUser CurrentUser(this Controller @this)
        {
            SOFAUserManager userManager = @this.HttpContext.GetOwinContext()
                                               .GetUserManager<SOFAUserManager>();
            SOFAUser user = userManager.FindByName(@this.HttpContext.User.Identity.Name);
            return user;
        }

        public static SOFAUser CurrentUser(this HtmlHelper @this)
        {
            HttpContext current = HttpContext.Current;
            SOFAUserManager userManager = current.GetOwinContext().GetUserManager<SOFAUserManager>();
            SOFAUser user = userManager.FindByName(current.User.Identity.Name);
            return user;
        }

        public static Person CurrentUserPerson(this HtmlHelper @this)
        {
            HttpContext current = HttpContext.Current;
            DBContext dbCon = new DBContext();
            SOFAUser user = @this.CurrentUser();

            return dbCon.Persons.Where(person => person.User.Id == user.Id).First();
        }

        public static bool IsInRoles(this SOFAUserManager @this, string userId
                                    ,string roles)
        {
            return @this.GetRoles(userId).Intersect(roles.Split(',')).Count() > 0;
        }

        public static void resultToModelState(ModelStateDictionary dest
                                             ,IdentityResult result)
        {
            foreach(String error in result.Errors)
                dest.AddModelError("", error);         
        }

        public static SOFARoleManager RoleManager(this Controller @this)
        {
            return @this.HttpContext.GetOwinContext().Get<SOFARoleManager>();
        }

        public static SOFAUserManager UserManager(this Controller @this)
        {
            return @this.HttpContext.GetOwinContext().GetUserManager<SOFAUserManager>();
        }

        public static SOFAUserManager UserManager(this HtmlHelper @this)
        {
            HttpContext context = HttpContext.Current;
            return context.GetOwinContext().GetUserManager<SOFAUserManager>();
        }
    }
}