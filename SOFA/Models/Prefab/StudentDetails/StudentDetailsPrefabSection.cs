using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SOFA.Models.Prefab;

namespace SOFA.Models.Prefab.StudentDetails
{
    public class StudentDetailsPrefabSection : PrefabSection
    {
       
        private const string NAME = "Student Information";

        private Section section;

        public StudentDetailsPrefabSection()
        {
            Id = PrefabSection.STUDENT_DETAILS;
            Name = NAME;            
        }

        public override Section GetSection()
        {            
            if (section == null)
            {
                section = new Section();
                section.Id = Id;
                section.Name = NAME;
                section.DateCreated = DateTime.Now;
                var fieldfactory = new PrefabFieldFactory();
                section.Fields.Add(fieldfactory.Get(PrefabField.FIRSTNAME));
                section.Fields.Add(fieldfactory.Get(PrefabField.LASTNAME));
                section.Fields.Add(fieldfactory.Get(PrefabField.PHONE_NUMBER));
                section.Fields.Add(fieldfactory.Get(PrefabField.MOBILE_NUMBER));
                section.Fields.Add(fieldfactory.Get(PrefabField.STUDENT_EMAIL));
                
            }

            return section;
        }
    }
}