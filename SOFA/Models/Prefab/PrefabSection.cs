﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOFA.Models;

namespace SOFA.Models.Prefab
{
    public abstract class PrefabSection
    {
        private const string STUDENT_DETAILS = "a9bd91b0-29b5-11e4-8c21-0800200c9a66";

        private string Id;
        private string Name;

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
