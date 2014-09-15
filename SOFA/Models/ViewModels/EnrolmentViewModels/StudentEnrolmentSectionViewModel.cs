using SOFA.Models.Prefab;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOFA.Models.ViewModels.EnrolmentViewModels
{
    public class StudentEnrolmentSectionViewModel : EnrolmentSectionViewModel
    {
        public Student Student { get; set; }

        public StudentEnrolmentSectionViewModel(): base()
        { }

        public StudentEnrolmentSectionViewModel(EnrolmentSection section): base(section)
        {
            if (!section.OriginalSectionId.Equals(PrefabSection.STUDENT_DETAILS)
                throw new ArgumentOutOfRangeException("Enrolment section not a Student details section");
        }

        

    }
}