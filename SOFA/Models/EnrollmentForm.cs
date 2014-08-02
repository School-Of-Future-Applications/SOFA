using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SOFA.Models
{
    public class EnrollmentForm : IValidatableObject
    {

        [Key]
        public String Id { get; set; }

        public String Name { get; set; }

        public DateTime DateCreated { get; set; }

        public virtual IEnumerable<EnrollmentFormSection> EnrollmentFormSections { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}