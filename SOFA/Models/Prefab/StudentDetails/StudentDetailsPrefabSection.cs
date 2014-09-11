using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOFA.Models.Prefab
{
    public class StudentDetailsPrefabSection : PrefabSection
    {
       
        private const string NAME = "Student Information";

        private Section section;

        public StudentDetailsPrefabSection(string id) : base(id)
        {
            Name = NAME;
        }

        public override Section GetSection()
        {            
            if (section == null)
            {
                section = new Section();
                section.Id = Id;
                section.Name = NAME;
                
            }
        }
    }
}