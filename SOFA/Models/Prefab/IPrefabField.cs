using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOFA.Models.Prefab
{
    interface IPrefabField
    {
        public string GetId();

        public string GetName();

        public Field GetField();

    }
}
