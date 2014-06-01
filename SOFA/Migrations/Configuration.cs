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
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using SOFA.Models;
using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;

namespace SOFA.Migrations
{

    internal sealed class Configuration : DbMigrationsConfiguration<SOFA.Models.DBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true; 
        }

        protected override void Seed(SOFA.Models.DBContext context)
        {
            string[] usernames = { "teacher", "sysadmin","sofaadmin","moderator" };
            string[] rolenames = {"SystemAdmin","SOFAAdmin","Moderator","Teacher"};
            var rm = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var um = new UserManager<IdentityUser>(new UserStore<IdentityUser>(context));

            //add default users
            foreach(string uname in usernames)
            {
                var user = new IdentityUser() { UserName = uname };
                um.Create(user, uname);
            }
            
            //add default roles
            foreach(string r in rolenames)
            {
                if (!rm.RoleExists(r))
                    rm.Create(new IdentityRole(r));
            }

            context.SaveChanges();

            //add roles to users
            um.AddToRole(um.FindByName("sysadmin").Id, "SystemAdmin");
            um.AddToRole(um.FindByName("sysadmin").Id, "SOFAAdmin");
            um.AddToRole(um.FindByName("sysadmin").Id, "Moderator");
            um.AddToRole(um.FindByName("teacher").Id, "Teacher");
            um.AddToRole(um.FindByName("sofaadmin").Id, "SOFAAdmin");
            um.AddToRole(um.FindByName("sofaadmin").Id, "Moderator");
            um.AddToRole(um.FindByName("moderator").Id, "Moderator");
            
            context.SaveChanges();
        }
    }
}
