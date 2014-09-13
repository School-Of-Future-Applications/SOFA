using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOFA.Models.Prefab.StandardSection
{
    public class StandardPrefabSection : PrefabSection
    {
        public const string NAME = "Standard Section";

        private Section section;

        public StandardPrefabSection()
        {
            this.Id = PrefabSection.STANDARD_SECTION;
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

                section.Fields.Add(factory.Get(PrefabField.TESTFIELD_A));
                section.Fields.Add(factory.Get(PrefabField.TESTDATEFIELD_A));                
            }

            return section;
        }
    }
}