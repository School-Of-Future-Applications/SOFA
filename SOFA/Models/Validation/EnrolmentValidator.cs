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
                yield return new ValidationResult("Not a valid response", new List<String>()
                    {
                        "Value"
                    });
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

        public static Boolean IsValidDate(EnrolmentField field)
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

        public static Boolean IsValidDropdown(EnrolmentField field)
        {
            var possibleResponses = field.EnrollmentFieldOptions.
                                        Where(o => o.OptionType.Equals(FieldOption.OPT_RESPONSE)).
                                        Select(o => o.OptionValue).ToList();
            return possibleResponses.Contains(field.Value);
        }

        #endregion

        

        #region Option Validation

        public static Boolean IsValidMandatory(EnrolmentField field)
        {
            return !String.IsNullOrWhiteSpace(field.Value);                      
        }

        public static Boolean IsValidNumeric(EnrolmentField field)
        {
            return !String.IsNullOrEmpty(field.Value) && Regex.IsMatch(field.Value, @"^\d[\d.]+$");
        }
        //TODO IsValid for other option types

        #endregion
    }
}