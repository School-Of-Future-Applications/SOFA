using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SOFA.Models
{
    public class EnrolmentSection
    {
        [Key]
        public int EnrolmentSectionId { get; set; }

        public DateTime DateCreated { get; set; }

        public String SectionName { get; set; }

        public virtual IEnumerable<EnrolmentField> EnrollmentFields { get; set; }

        public virtual ICollection<EnrolmentFormSection> EnrolmentFormSections { get; set; }
    }
}