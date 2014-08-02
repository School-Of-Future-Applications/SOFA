using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOFA.Models
{
    public class EnrollmentFormSection 
    {
        public virtual EnrollmentSection Section { get; set; }

        public virtual EnrollmentSection BelowOf { get; set; }
    }
}