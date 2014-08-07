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
            SOFAUser God = new SOFAUser();
            Person Gods_Info = new Person();
            SOFARoleManager srm = new SOFARoleManager(new RoleStore<SOFARole>(context));
            SOFAUserManager sum = new SOFAUserManager(new UserStore<SOFAUser>(context));

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
            FormSeed(context);
            
        }

        private void FormSeed(DBContext context)
        {
            //TODO Seed method for form meta data
            //Create form and form sections
            Form form = new Form()
            {
                FormName = "Seed Form",
            };
            FormSection fSectionA = new FormSection();
            FormSection fSectionB = new FormSection();
            form.FormSections.Add(fSectionA);
            form.FormSections.Add(fSectionB);

            //Create Sections
            Section sectionA = new Section()
            {
                DateCreated = DateTime.Now,
                Name = "Section A"
            };

            Section sectionB = new Section()
            {
                DateCreated = DateTime.Now,
                Name = "Section B"
            };
            //Set order of sections
            fSectionA.Section = sectionA;
            fSectionA.BelowOf = null;
            fSectionB.Section = sectionB;
            fSectionB.BelowOf = sectionA;

            //Create fields
            Field fieldA = new Field(Field.TYPE_TEXT_FIELD);
            Field fieldB = new Field(Field.TYPE_TEXT_FIELD);
            Field fieldC = new Field(Field.TYPE_TEXT_FIELD);
            Field fieldD = new Field(Field.TYPE_TEXT_FIELD);

            //Add options to fields
            fieldA.FieldOptions.Add(new FieldOption(FieldOption.OPT_MANDATORY));
            fieldB.FieldOptions.Add(new FieldOption(FieldOption.OPT_MANDATORY));
            fieldC.FieldOptions.Add(new FieldOption(FieldOption.OPT_NUMERIC));
            fieldC.FieldOptions.Add(new FieldOption(FieldOption.OPT_MANDATORY));
            
            //Add fields to sections
            sectionA.Fields.Add(fieldA);
            sectionA.Fields.Add(fieldB);
            sectionB.Fields.Add(fieldC);
            sectionB.Fields.Add(fieldD);

            context.Forms.Add(form);
            context.SaveChanges();

        }
    }
}
