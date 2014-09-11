using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOFA.Models;

namespace SOFA.Models.Prefab
{
    public abstract class PrefabSection
    {
        public const string STUDENT_DETAILS = "a9bd91b0-29b5-11e4-8c21-0800200c9a66";
        public const string COURSE_SELECT = "a9bd91b1-29b5-11e4-8c21-0800200c9a66";


        protected string Id;
        protected string Name;

        public PrefabSection(string id)
        {
            this.Id = id;
        }

        public virtual string GetId()
        {
            return Id;
        }

        public virtual string GetName()
        {
            return Name;
        }

        public abstract Section GetSection();

    }
}
