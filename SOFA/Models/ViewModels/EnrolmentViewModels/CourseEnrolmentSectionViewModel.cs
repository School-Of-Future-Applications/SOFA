using SOFA.Models.Prefab;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SOFA.Models.ViewModels.EnrolmentViewModels
{
    public class CourseEnrolmentSectionViewModel : EnrolmentSectionViewModel
    {
        public IEnumerable<SelectListItem> DepartmentSelect { get; set; }

        public IEnumerable<SelectListItem> CourseSelect { get; set; }

        public IEnumerable<SelectListItem> YearLevelSelect { get; set; }

        public int SelectedClassId { get; set; }
                
        public CourseEnrolmentSectionViewModel(): base()
        { }

        public CourseEnrolmentSectionViewModel(EnrolmentSection section, List<Department> departments) : base(section)
        { 
            if (section.OriginalSectionId != PrefabSection.COURSE_SELECT)
            {
                throw new ArgumentOutOfRangeException("Enrolment Section not a course select section");
            }
            DepartmentSelect = new SelectList(departments, "Id", "Name");

            //At this stage just display the departments
            CourseSelect = new SelectList(Enumerable.Empty<SelectListItem>());
            YearLevelSelect = new SelectList(Enumerable.Empty<SelectListItem>());

        }




    }
}