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
            if (!Field.FieldTypes().Contains(field.FieldType))
            {
                yield return new ValidationResult("Illegal field type", new List<String> { "FieldType" });
            }            
            //Validate option types against field type

            yield return null;
        }

        public static IEnumerable<ValidationResult> ValidateFieldOption(FieldOption fieldOption)
        {
            //Validate option types
            if (!FieldOption.FieldOptionTypes().Contains(fieldOption.OptionType))
            {
                yield return new ValidationResult("Illegal option type,", new List<String> { "OptionType" });
            }

            //TODO: Validate option values vs. option type

        }
    }
}