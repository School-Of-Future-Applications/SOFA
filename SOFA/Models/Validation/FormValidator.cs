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
            yield return null;
        }

        /**
         * Validates the correctness of a Field and its FieldOptions.
         */
        public static IEnumerable<ValidationResult> ValidateField(Field field)
        { 
            //TODO
            //Validate Field Type
            if (!Field.FIELD_TYPES.Contains(field.FieldType))
            {
                yield return new ValidationResult("Illegal field type", new List<String> { "FieldType" });
            }            
            //Validate option types against field type
            foreach (FieldOption fo in field.FieldOptions)
            {
                if (!IsOptionTypeValid(fo.OptionType, field.FieldType))
                {
                    yield return new ValidationResult(
                            String.Format("{0} cannot be a constraint on a field of type {1}", 
                                          fo.OptionType, field.FieldType));
                }                
            }
            yield break;
        }

        public static IEnumerable<ValidationResult> ValidateFieldOption(FieldOption fieldOption)
        {
            //Validate option types
            if (!FieldOption.FieldOptionTypes().Contains(fieldOption.OptionType))
            {
                yield return new ValidationResult("Illegal option type,", new List<String> { "OptionType" });
            }

            //Validate option values vs. option type
            if (!IsOptionValueValid(fieldOption.OptionType, fieldOption.OptionValue))
            {
                yield return new ValidationResult(
                    String.Format("Invalid value set for option type {0}",fieldOption.OptionType));
            }

            yield break;
        }

        private static Boolean IsOptionTypeValid(String optionType, String fieldType)
        {
            //TODO
            return true;
        }

        #region Option Value Validators

        private static Boolean IsOptionValueValid(String optionType, String optionValue)
        {
            if (optionType == FieldOption.OPT_MANDATORY &&
                !isOptValueValid_Mandatory(optionValue))
            {
                return false;
            }
            else if (optionType == FieldOption.OPT_NUMERIC &&
                !isOptValueValid_Numeric(optionValue))
            {
                return false;
            }

            return true;
        }

        private static Boolean isOptValueValid_Numeric(String value)
        {
            //TODO
            return true;
        }

        private static Boolean isOptValueValid_Mandatory(String value)
        {
            //TODO
            return true;
        }

        #endregion
    }
}