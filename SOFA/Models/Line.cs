using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOFA.Models
{
    public class Line
    {
        public int Id { get; set; }
        public virtual ICollection<LineTime> LineTimes { get; set; }
        public virtual Timetable Timetable { get; set; }     
    }
}