using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SOFA.Models
{
    public class EnrolmentFormSection 
    {
        [Key, Column(Order = 1)]
        public String EnrollmentFormId { get; set; }

        [Key, Column(Order = 2)]
        public String EnrollmentSectionId { get; set; }

        public virtual EnrolmentForm Form { get; set; }

        public virtual EnrollmentSection Section { get; set; }

        [Required]
        public virtual EnrollmentSection BelowOf { get; set; }
    }
}