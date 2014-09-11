using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOFA.Models.Prefab
{
    public class StudentDetailsPrefabSection : PrefabSection
    {
        private const string STUDENT_SECTION_ID = "a9bd91b0-29b5-11e4-8c21-0800200c9a66";
        private const string STUDENT_SECTION_NAME = "Student Information";


        public string GetId()
        {
            return STUDENT_SECTION_ID;
        }

        public string GetName()
        {
            return STUDENT_SECTION_NAME;
        }

        public Section GetSection()
        {
            throw new NotImplementedException();
        }
    }
}