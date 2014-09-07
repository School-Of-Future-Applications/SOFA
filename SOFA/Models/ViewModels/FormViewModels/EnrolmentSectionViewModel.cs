using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SOFA.Models.ViewModels.FormViewModels
{
    public class EnrolmentSectionViewModel : EnrolmentSection, IValidatableObject
    {
        
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}