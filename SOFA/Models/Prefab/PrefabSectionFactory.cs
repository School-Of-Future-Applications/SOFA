using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SOFA.Models.Prefab.StudentDetails;
using SOFA.Models.Prefab.StandardSection;
using SOFA.Models.Prefab.CourseSelect;

namespace SOFA.Models.Prefab
{
    public class PrefabSectionFactory
    {
        public Section Get(string sectionId)
        {
            if (sectionId.Equals(PrefabSection.STUDENT_DETAILS))
                return new StudentDetailsPrefabSection().GetSection();
            else if (sectionId.Equals(PrefabSection.STANDARD_SECTION))
                return new StandardPrefabSection().GetSection();
            else if (sectionId.Equals(PrefabSection.COURSE_SELECT))
                return new CourseSelectPrefabSection().GetSection();


            else
                throw new ArgumentException("Undefined prefab section");
            
        }
    }
}