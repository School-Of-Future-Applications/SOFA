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
using Microsoft.Owin.Security.Cookies;
using Owin;

using SOFA.Infrastructure.Users;
using SOFA.Models;

namespace SOFA.App_Start
{
    public class IdentityConfig
    {
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext<DBContext>(DBContext.Create);
            app.CreatePerOwinContext<SOFAUserManager>(SOFAUserManager.Create);
            app.CreatePerOwinContext<SOFARoleManager>(SOFARoleManager.Create);

            app.UseCookieAuthentication(new CookieAuthenticationOptions
                {
                    AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie
                   ,LoginPath = new PathString("/Login/UserLogin")
                   ,
                    Provider = new CookieAuthenticationProvider
                    {      
                        OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<SOFAUserManager,SOFAUser>(System.TimeSpan.FromMinutes(1),regenerateIdentity: (manager,user) => user.GenerateUserIdentityAsync(manager))          
                    } 
                });
        }
    }
}