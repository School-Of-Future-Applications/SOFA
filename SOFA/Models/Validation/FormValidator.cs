using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SOFA.Models.Validation
{
    public static class FormValidator
    {
        /**
         * Validates the correctness of a form object.
         */
        public IEnumerable<ValidationResult> ValidateForm(Form form)
        {
            //TODO
            return null;
        }

        /**
         * Validates the correctness of a Field and its FieldOptions.
         */
        public IEnumerable<ValidationResult> ValidateField(Field field)
        {
            //TODO
            return null;
        }
    }
}