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
        public EnrolmentFormSection(EnrolmentForm eForm, FormSection section)
        {
            EnrolmentFormId = eForm.EnrolmentFormId;
            EnrolmentForm = eForm;
            fromFormSection(section);
        }

        [Key, Column(Order = 1)]
        public String EnrolmentFormId { get; set; }

        [Key, Column(Order = 2)]
        public String EnrolmentSectionId { get; set; }

        public virtual EnrolmentForm EnrolmentForm { get; set; }

        public virtual EnrolmentSection EnrolmentSection { get; set; }

        public virtual EnrolmentSection BelowOf { get; set; }

        private void fromFormSection(FormSection section)
        {
        
        }
    }
}