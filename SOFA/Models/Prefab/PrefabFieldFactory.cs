using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SOFA.Models.Prefab.StudentDetails;

namespace SOFA.Models.Prefab
{
    public class PrefabFieldFactory
    {
        public  PrefabField Get(string fieldId)
        {
            PrefabField field;
            if (fieldId.Equals(PrefabField.FIRSTNAME))
                return new FirstName();
            return null;
        }
    }
}