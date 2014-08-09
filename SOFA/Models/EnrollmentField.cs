using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SOFA.Models
{
    public class EnrollmentField : IValidatableObject
    {
        [Key]
        public int Id { get; set; }
        public String FieldType { get; set; }
        public String PromptValue { get; set; }
        public virtual List<EnrollmentFieldOption> EnrollmentFieldOptions { get; set; }
        public String Value { get; set; }

        public EnrollmentField()
        {
            EnrollmentFieldOptions = new List<EnrollmentFieldOption>();
        }

        public EnrollmentField(Field field) : this()
        {
            FieldType = field.FieldType;
            PromptValue = field.PromptValue;

            foreach (FieldOption opt in field.FieldOptions)
            {
                EnrollmentFieldOptions.Add(new EnrollmentFieldOption(opt));                
            }
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}