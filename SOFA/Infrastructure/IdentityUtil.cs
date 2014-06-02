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
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

using SOFA.Infrastructure.Users;

namespace SOFA.Infrastructure
{
    public static class IdentityUtil
    {
        public static IAuthenticationManager AuthManager(this Controller @this)
        {
            return @this.HttpContext.GetOwinContext().Authentication;
        }

        public static void resultToModelState(ModelStateDictionary dest
                                             ,IdentityResult result)
        {
            foreach(String error in result.Errors)
                dest.AddModelError("", error);         
        }

        public static SOFAUserManager UserManager(this Controller @this)
        {
            return @this.HttpContext.GetOwinContext().GetUserManager<SOFAUserManager>();
        }
    }
}