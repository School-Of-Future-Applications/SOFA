using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SOFA.Models
{
    public class DBContext : DbContext
    {
        public DBContext() : base("DefaultConnection")
        {

        }

        public DbSet<Line> Lines { get; set; }
        public DbSet<ClassBase> ClassBases { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<LineTime> LineTimes { get; set; }

        public DbSet<Teacher> Teachers { get; set; }

        public DbSet<Timetable> Timetables { get; set; }

        public DbSet<TimetabledClass> TimetabledClasses { get; set; }
    }
}