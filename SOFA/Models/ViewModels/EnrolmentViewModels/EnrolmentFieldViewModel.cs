using SOFA.Models.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SOFA.Models.ViewModels.EnrolmentViewModels
{
    public class EnrolmentFieldViewModel : EnrolmentField, IValidatableObject
    {

        public EnrolmentField toEnrolmentField()
        {
            var eField = new EnrolmentField()
            {
                EnrolmentFieldId = this.EnrolmentFieldId,
                PromptValue = this.PromptValue,
                Value = this.Value,
                FieldType = this.FieldType,
                EnrollmentFieldOptions = this.EnrollmentFieldOptions
            };

            return eField;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return EnrolmentValidator.ValidateField(this); 
        }
    }
}