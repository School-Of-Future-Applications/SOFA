using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOFA.Models;

namespace SOFA.Models.Prefab
{
    interface IPrefabSection
    {
        public string GetId();

        public string GetName();

        public Section GetSection();

    }
}
