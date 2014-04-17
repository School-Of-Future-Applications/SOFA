using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SOFA.Models
{
    public class LineTime
    {
        [Key]
        public int Id { get; set; }

        public Timetable Timetable { get; set; }

        public List<LineTime> LineTimes { get; set; }

        public Int32 Day { get; set; }

        public Int32 Time { get; set; }
    }
}