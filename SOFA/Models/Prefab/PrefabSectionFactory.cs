using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SOFA.Models.Prefab.StudentDetails;

namespace SOFA.Models.Prefab
{
    public class PrefabSectionFactory
    {
        public Section Get(string sectionId)
        {
            if (sectionId.Equals(PrefabSection.STUDENT_DETAILS))
                return new StudentDetailsPrefabSection().GetSection();
            else
                throw new ArgumentException("Undefined prefab section");
            
        }
    }
}