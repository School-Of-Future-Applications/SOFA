using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace SOFA.Models.Validation
{
    public static class EnrolmentValidator
    {
        public static IEnumerable<ValidationResult> ValidateForm(EnrolmentForm form)
        {
            yield return ValidationResult.Success;
        }

        public static IEnumerable<ValidationResult> ValidateField(EnrolmentField field)
        {
            
            //Test value against field type
            if (field.FieldType.Equals(Field.TYPE_DATE) &&
                !IsValidDate(field))
            {
                yield return new ValidationResult("Not a valid date", new List<String>()
                    {
                        "Value"
                    });
            }
            if (field.FieldType.Equals(Field.TYPE_DROPDOWN) &&
                !IsValidDropdown(field))
            {
                
            }
            
            //Test value against option types
            var optionTypes = field.EnrollmentFieldOptions.ToList();
            foreach (var ot in optionTypes)
            {
                if (ot.OptionType.Equals(FieldOption.OPT_MANDATORY) && 
                    ot.OptionValue.Equals(FieldOption.VAL_TRUE) &&
                    !IsValidMandatory(field))
                {
                    yield return new ValidationResult("Field is mandatory", new List<String>()
                        {
                            "Value"
                        });
                }
                if (ot.OptionType.Equals(FieldOption.OPT_NUMERIC) && 
                    ot.OptionValue.Equals(FieldOption.VAL_TRUE) &&
                    !IsValidNumeric(field))
                {
                    yield return new ValidationResult("Field must be a number", new List<string>()
                    {
                        "Value"
                    });
                }
                

            }
            yield break;
        }

        #region Data Type Validation

        private static Boolean IsValidDate(EnrolmentField field)
        {
            try
            {
                var culture = CultureInfo.CurrentCulture;
                DateTime.Parse(field.Value, culture);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static Boolean IsValidDropdown(EnrolmentField field)
        {
            return true;
        }

        #endregion

        

        #region Option Validation

        private static Boolean IsValidMandatory(EnrolmentField field)
        {
            if (field.Value != null &&
                field.Value.Trim().Count() > 0)
            {
                return true;
            }

            return false;
            
        }

        private static Boolean IsValidNumeric(EnrolmentField field)
        {
            return Regex.IsMatch(field.Value, @"^\d[\d.]+$");
        }
        //TODO IsValid for other option types

        #endregion
    }
}