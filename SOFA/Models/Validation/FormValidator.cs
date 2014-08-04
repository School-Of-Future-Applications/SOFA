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
        public static IEnumerable<ValidationResult> ValidateForm(Form form)
        {
            //TODO
            return null;
        }

        /**
         * Validates the correctness of a Field and its FieldOptions.
         */
        public static IEnumerable<ValidationResult> ValidateField(Field field)
        { 
            //TODO
            //Validate Field Type

            //Validate option types

            //Validate option types against field type

            return null;
        }
    }
}