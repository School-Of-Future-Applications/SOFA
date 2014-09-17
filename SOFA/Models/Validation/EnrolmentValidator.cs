using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
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
            
            //Test value against option types
            var optionTypes = field.EnrollmentFieldOptions.Select(f => f.OptionType).ToList();
            foreach (var ot in optionTypes)
            {
                if (ot.Equals(FieldOption.OPT_MANDATORY) &&
                    !IsValidMandatory(field))
                {
                    yield return new ValidationResult("Field is mandatory", new List<String>()
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

        //TODO IsValid for other option types

        #endregion
    }
}