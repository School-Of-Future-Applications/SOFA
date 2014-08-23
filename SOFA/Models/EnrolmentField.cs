using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SOFA.Models
{
    public class EnrolmentField : IValidatableObject
    {
        [Key]
        public int Id { get; set; }
        public String FieldType { get; set; }
        public String PromptValue { get; set; }
        public virtual List<EnrolmentFieldOption> EnrollmentFieldOptions { get; set; }
        public String Value { get; set; }

        public EnrolmentField()
        {
            EnrollmentFieldOptions = new List<EnrolmentFieldOption>();
        }

        public EnrolmentField(Field field) : this()
        {
            FieldType = field.FieldType;
            PromptValue = field.PromptValue;

            foreach (FieldOption opt in field.FieldOptions)
            {
                EnrollmentFieldOptions.Add(new EnrolmentFieldOption(opt));                
            }
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}