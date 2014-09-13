using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOFA.Models.Prefab.CourseSelect
{
    public class CourseSelectPrefabSection : PrefabSection
    {
        private const string NAME = "Enrolled Class";

        private Section section;

        public CourseSelectPrefabSection()
        {
            this.Id = PrefabSection.COURSE_SELECT;
            this.Name = NAME;
        }

        public override Section GetSection()
        {
            if (this.section == null)
            {
                this.section = new Section();
                section.Id = this.Id;
                section.Name = this.Name;

                PrefabFieldFactory factory = new PrefabFieldFactory();

                section.Fields.Add(factory.Get(PrefabField.CLASS_SELECT));
            }

            return section;
        }
    }
}