using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOFA.Models.Prefab
{
    public class StudentDetailsPrefabSection : PrefabSection
    {
        
        private const string NAME = "Student Information";

        public StudentDetailsPrefabSection(string id) : base(id)
        {
            Name = NAME;
        }

        public override Section GetSection()
        {
            throw new NotImplementedException();
        }
    }
}