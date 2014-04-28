using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SOFA.Models;

namespace SOFA.Models.ViewModels
{
    public class TimetableIndexViewModel
    {
        public IEnumerable<Timetable> CurrentTimetables { get; set; }

        public IEnumerable<Timetable> OldTimetables { get; set; }

        public TimetableIndexViewModel(IEnumerable<Timetable> tlist)
        {
            DateTime now = System.DateTime.Now;
            CurrentTimetables = tlist.Where(t => t.ExpiryDate.CompareTo(now) > 0);
            OldTimetables = tlist.Except(CurrentTimetables);
        }

    }
}