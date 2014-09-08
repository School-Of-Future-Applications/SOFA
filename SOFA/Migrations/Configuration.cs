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
            IdentitySeed(context);            
                       
        }

        private void IdentitySeed(DBContext context)
        {
            SeedSysAdmin(context);
            SeedClient(context);   
        }

        private void FormSeed(DBContext context)
        {
            List<Section> DBSections = context.Sections.ToList();
            if (!DBSections.Any(s => s.Id == Section.STUDENT_SECTION_ID ||
                                    s.Id == Section.COURSE_SECTION_ID))
            {
                //Create form and form sections
                Form form = new Form()
                {
                    FormName = "Seed Form",
                };

                FormSection fSectionCourse = new FormSection();
                FormSection fSectionStudent = new FormSection();
                form.FormSections.Add(fSectionCourse);
                form.FormSections.Add(fSectionStudent);

                //Create Sections
                Section sectionCourse = new Section()
                {
                    DateCreated = DateTime.Now,
                    Name = Section.COURSE_SECTION_NAME
                };
                sectionCourse.Id = Section.COURSE_SECTION_ID;
                Section sectionStudent = new Section()
                {
                    DateCreated = DateTime.Now,
                    Name = Section.STUDENT_SECTION_NAME
                };
                sectionStudent.Id = Section.STUDENT_SECTION_ID;
                //Set order of sections
                fSectionCourse.Section = sectionCourse;
                fSectionCourse.BelowOf = null;
                fSectionStudent.Section = sectionStudent;
                fSectionStudent.BelowOf = sectionCourse;

                context.Forms.Add(form);
                context.SaveChanges();

                //Create fields
                Field fieldA = new Field(Field.TYPE_TEXT_SINGLE)
                {
                    Section = sectionCourse,
                    PromptValue = "Field A"
                };
                Field fieldB = new Field(Field.TYPE_TEXT_SINGLE)
                {
                    Section = sectionCourse,
                    PromptValue = "Field B"
                };
                Field fieldC = new Field(Field.TYPE_TEXT_SINGLE)
                {
                    Section = sectionStudent,
                    PromptValue = "Field C"
                };
                Field fieldD = new Field(Field.TYPE_TEXT_SINGLE)
                {
                    Section = sectionStudent,
                    PromptValue = "Field D"
                };
                Field fieldE = new Field(Field.TYPE_INFO)
                {
                    Section = sectionStudent,
                    PromptValue = "This is a info box. It will have lots and lots of text."
                };
                Field fieldF = new Field(Field.TYPE_TEXT_MULTI)
                {
                    Section = sectionStudent,
                    PromptValue = "Enter some multi text"
                };
                Field fieldG = new Field(Field.TYPE_DATE)
                {
                    Section = sectionStudent,
                    PromptValue = "Birthday"
                };

                //Add options to fields
                fieldA.FieldOptions.Add(new FieldOption(FieldOption.OPT_MANDATORY));
                fieldB.FieldOptions.Add(new FieldOption(FieldOption.OPT_MANDATORY));
                fieldC.FieldOptions.Add(new FieldOption(FieldOption.OPT_NUMERIC));
                fieldC.FieldOptions.Add(new FieldOption(FieldOption.OPT_MANDATORY));

                //Add fields to sections
                sectionCourse.Fields.Add(fieldA);
                sectionCourse.Fields.Add(fieldB);
                sectionStudent.Fields.Add(fieldC);
                sectionStudent.Fields.Add(fieldE);
                sectionStudent.Fields.Add(fieldF);
                sectionStudent.Fields.Add(fieldD);
                sectionStudent.Fields.Add(fieldG);

                context.Fields.AddRange(new List<Field>() 
            {
                fieldA,
                fieldB,
                fieldC,
                fieldD
            });
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

                sum.Create(Client, "kgough13");

                context.SaveChanges();

                Client_Info.GivenNames = "Kathie";
                Client_Info.LastName = "Gough";
                Client_Info.Position = "Client";
                Client_Info.Email = "kgoug13@eq.edu.au";
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
