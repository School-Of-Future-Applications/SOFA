using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

using SOFA.Infrastructure;

namespace SOFA.Models
{
    public class EnrolmentForm : IValidatableObject
    {
        public EnrolmentForm()
        {
            Id = UUIDUtil.NewUUID();
            DateCreated = DateTime.Now;
            EnrolmentFormSections = new List<EnrolmentFormSection>();
        }

        [Key]
        public String Id { get; set; }

        public String Name { get; set; }

        public DateTime DateCreated { get; set; }

        public Student Student { get; set; }

        public TimetabledClass Class { get; set; }

        public virtual IEnumerable<EnrolmentFormSection> EnrolmentFormSections { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}