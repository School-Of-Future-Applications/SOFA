using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SOFA.Models
{
    public class EnrollmentSection
    {
        public String ID { get; set; }

        public DateTime DateCreated { get; set; }

        public String SectionName { get; set; }

        public virtual IEnumerable<EnrollmentField> EnrollmentFields { get; set; }
    }
}