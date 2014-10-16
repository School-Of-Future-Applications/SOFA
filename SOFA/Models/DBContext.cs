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

using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace SOFA.Models
{
    public class DBContext : IdentityDbContext<SOFAUser>
    {
        public DBContext() : base("DefaultConnection",throwIfV1Schema:false)
        { }

        public DbSet<Department> Departments { get; set; }

        public DbSet<ClassBase> ClassBases { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Line> Lines { get; set; }

        public DbSet<LineTime> LineTimes { get; set; }

        public DbSet<Person> Persons { get; set; }

        public DbSet<Student> Students { get; set; }

        public DbSet<Timetable> Timetables { get; set; }

        public DbSet<TimetabledClass> TimetabledClasses { get; set; }

        #region Enrollment 

        public DbSet<Form> Forms { get; set; }

        public DbSet<FormSection> FormSections { get; set; }

        public DbSet<Section> Sections { get; set; }

        public DbSet<Field> Fields { get; set; }

        public DbSet<SectionFieldOrder> SectionFieldOrders { get; set; }

        public DbSet<FieldOption> FieldOptions { get; set; }

        public DbSet<EnrolmentForm> EnrolmentForms { get; set; }

        public DbSet<EnrolmentFormSection> EnrolmentFormSections { get; set; }

        public DbSet<EnrolmentSection> EnrolmentSections { get; set; }

        public DbSet<EnrolmentField> EnrolmentFields { get; set; }

        public DbSet<EnrolmentFieldOption> EnrolmentFieldOptions { get; set; }

        public DbSet<File> Files { get; set; }

        #endregion

        public static DBContext Create()
        {
            return new DBContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Section>().
                HasMany(s => s.Fields).
                WithOptional(f => f.Section).
                WillCascadeOnDelete(true);
            modelBuilder.Entity<Field>().
                HasMany(f => f.FieldOptions).
                WithOptional(fo => fo.Field).
                WillCascadeOnDelete(true);


          
            
        }
    }
}