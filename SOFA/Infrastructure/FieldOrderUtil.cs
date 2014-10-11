using SOFA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOFA.Infrastructure
{
    public static class FieldOrderUtil
    {
        public static List<Field> Sort(this List<Field> @this, DBContext context)
        {
            return new List<Field>();
        }

        public static List<EnrolmentField> Sort(this List<Field> @this, DBContext context)
        {
            return new List<EnrolmentField>();
        }
    }
}