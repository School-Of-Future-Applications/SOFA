using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SOFA.Models.ViewModels
{
    public class TimetabledClassSelectViewModel
    {
        public List<SelectListItem> Timetables { get; set; }

        public int ClassTemplateID { get; set; }

        public String ClassTemplateName { get; set; }

        public String SelectedTimetable { get; set; }
    }
}