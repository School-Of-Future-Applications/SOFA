using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOFA.Models.ViewModels.EnrolmentViewModels
{
    public class ClassSelectViewModel
    {

        public List<LineClassDisplayModel> Lines { get; set; }


        
    }

    public class LineClassDisplayModel
    {
        public string LineDisplayName { get; set; }

        public List<LineTime> Times { get; set; }

        public List<TimetabledClassDisplayModel> Classes { get; set; }

        public LineClassDisplayModel()
        {
            Classes = new List<TimetabledClassDisplayModel>();
            Times = new List<LineTime>();
        }
    }

    public class TimetabledClassDisplayModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public TimetabledClassDisplayModel(TimetabledClass c)
        {
            Id = c.Id;
            Name = c.DisplayName;
        }
    }

    
}