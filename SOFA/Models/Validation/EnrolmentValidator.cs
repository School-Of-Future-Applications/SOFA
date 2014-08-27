using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SOFA.Models.Validation
{
    public static class EnrolmentValidator
    {
        public static IEnumerable<ValidationResult> ValidateForm(EnrolmentForm form)
        {
            yield return null;
        }

        public static IEnumerable<ValidationResult> ValidateField(EnrolmentField field)
        {
            yield return null;
        }
    }
}