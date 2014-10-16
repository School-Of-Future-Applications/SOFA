using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SOFA.Models.ViewModels
{
    public class StudentMoveViewModel
    {
        public int CurrentTimetabledClassId { get; set; }

        public List<String> EnrolmentFormIds { get; set; }

        public int NewTimetabledClassId { get; set; }

        public SelectList TimetabledClasses { get; set; }
 

        /* Default constructor */
        public StudentMoveViewModel()
        { }

        public StudentMoveViewModel(ICollection<TimetabledClass> Classes) : this()
        {
            TimetabledClasses = new SelectList(Classes, "Id", "DisplayName");
        }
    }
}