using SOFA.Models.Prefab;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOFA.Models.ViewModels.EnrolmentViewModels
{
    public class CourseEnrolmentSectionViewModel : EnrolmentSectionViewModel
    {
        public CourseEnrolmentSectionViewModel(): base()
        { }

        public CourseEnrolmentSectionViewModel(EnrolmentSection section) : base(section)
        { 
            if (section.OriginalSectionId != PrefabSection.COURSE_SELECT)
            {
                throw new ArgumentOutOfRangeException("Enrolment Section not a course select section");
            }
        }


    }
}