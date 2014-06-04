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
using Microsoft.Owin.Security.DataProtection;
using System;
using System.Web;
using System.Web.Mvc;

using SOFA.Infrastructure;
using SOFA.Models;

namespace SOFA.Infrastructure.Users
{
    public class SOFAUserManager : UserManager<SOFAUser>
    {
        public SOFAUserManager(IUserStore<SOFAUser> store)
            : base(store)
        { }

        public static SOFAUserManager Create(IdentityFactoryOptions<SOFAUserManager> options
                                            ,IOwinContext context)
        {
            DBContext dbContext = context.Get<DBContext>();
            SOFAUserManager manager =
                new SOFAUserManager(new UserStore<SOFAUser>(dbContext));

            manager.EmailService = new IdentityEmail();

            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
                manager.UserTokenProvider = new DataProtectorTokenProvider<SOFAUser>(dataProtectionProvider.Create("ASP.NET Identity"));

            return manager;
        }
    }
}