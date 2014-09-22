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
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

using SOFA.Infrastructure.Users;
using SOFA.Models;
using SOFA.Models.Prefab;

namespace SOFA.Migrations
{

    internal sealed class Configuration : DbMigrationsConfiguration<SOFA.Models.DBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true; 
        }

        protected override void Seed(DBContext context)
        {
            FormSeed(context); 
            UserSeed(context);            
                       
        }

        private void UserSeed(DBContext context)
        {
            SeedSysAdmin(context);
            SeedClient(context);   
        }

        private void FormSeed(DBContext context)
        {
            List<Section> DBSections = context.Sections.ToList();
            if (!DBSections.Any(s => s.Id == PrefabSection.STUDENT_DETAILS ||
                                    s.Id == PrefabSection.COURSE_SELECT ||
                                    s.Id == PrefabSection.STANDARD_SECTION))
            {
                //Create form and form sections
                Form form = new Form()
                {
                    FormName = "Seed Form",
                };
                PrefabSectionFactory sectionFactory = new PrefabSectionFactory();
                Section sectionStudent = sectionFactory.Get(PrefabSection.STUDENT_DETAILS);
                Section sectionCourse = sectionFactory.Get(PrefabSection.COURSE_SELECT);
                Section sectionStandard = sectionFactory.Get(PrefabSection.STANDARD_SECTION);

                FormSection fSectionCourse = new FormSection();
                FormSection fSectionStudent = new FormSection();
                FormSection fSectionStandard = new FormSection();
                form.FormSections.Add(fSectionCourse);
                form.FormSections.Add(fSectionStudent);
                form.FormSections.Add(fSectionStandard);


                //Set order of sections
                fSectionCourse.Section = sectionCourse;
                fSectionCourse.BelowOf = sectionStudent;
                fSectionStudent.Section = sectionStudent;
                fSectionStudent.BelowOf = sectionStandard;
                fSectionStandard.Section = sectionStandard;
                fSectionStandard.BelowOf = null;


                context.Forms.Add(form);
                context.SaveChanges();
            }

        }
               
        private void SeedSysAdmin(DBContext context)
        {
            SOFAUserManager sum = new SOFAUserManager(new UserStore<SOFAUser>(context));

            if (sum.FindByEmail("chuck_norris@asskicking.com") == null)
            {
                SOFARoleManager srm = new SOFARoleManager(new RoleStore<SOFARole>(context));
                SOFAUser God = new SOFAUser();
                Person Gods_Info = new Person();
                God.Active = true;
                God.Email = "chuck_norris@asskicking.com";
                God.EmailConfirmed = true;
                God.UserName = "chuck_norris@asskicking.com";

                sum.Create(God, "chucknorris");

                context.SaveChanges();

                Gods_Info.GivenNames = "Carlos Ray";
                Gods_Info.LastName = "Norris";
                Gods_Info.Position = "Ass Kicker";
                Gods_Info.Email = "chuck_norris@asskicking.com";
                Gods_Info.PhoneNumber = "0400000000";
                Gods_Info.User = God;

                context.Persons.Add(Gods_Info);
                context.SaveChanges();

                foreach (string role in SOFARole.SOFA_ROLES)
                    if (!srm.RoleExists(role))
                        srm.Create(new SOFARole(role));

                context.SaveChanges();

                sum.AddToRole(God.Id, SOFARole.SYSADMIN_ROLE);
                context.SaveChanges();
            }
            
        }

        private void SeedClient(DBContext context)
        {
            SOFAUserManager sum = new SOFAUserManager(new UserStore<SOFAUser>(context));

            if (sum.FindByEmail("kgoug13@eq.edu.au") == null)
            {
                SOFARoleManager srm = new SOFARoleManager(new RoleStore<SOFARole>(context));
                SOFAUser Client = new SOFAUser();
                Person Client_Info = new Person();
                Client.Active = true;
                Client.Email = "kgoug13@eq.edu.au";
                Client.EmailConfirmed = true;
                Client.UserName = "kgoug13@eq.edu.au";

                sum.Create(Client, "kgoug13");

                context.SaveChanges();

                Client_Info.GivenNames = "Kathie";
                Client_Info.LastName = "Gough";
                Client_Info.Position = "Client";
                Client_Info.Email = "kgoug13@eq.edu.au";
                Client_Info.PhoneNumber = "0400000000";
                Client_Info.User = Client;

                context.Persons.Add(Client_Info);
                context.SaveChanges();

                foreach (string role in SOFARole.SOFA_ROLES)
                    if (!srm.RoleExists(role))
                        srm.Create(new SOFARole(role));

                context.SaveChanges();

                sum.AddToRole(Client.Id, SOFARole.SOFAADMIN_ROLE);
                context.SaveChanges();
            }
        }

        
    }

 
}
