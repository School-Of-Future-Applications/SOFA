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
            yield return null;
        }

        public static IEnumerable<ValidationResult> ValidateField(EnrolmentField field)
        {
            yield return null;
        }

        #region Data Type Validation

        private static Boolean IsValidDate(EnrolmentField field)
        {
            try
            {
                var culture = CultureInfo.CurrentCulture;
                DateTime.ParseExact(field.Value, "dd/mm/yyyy", culture);
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
            if (field.Value.Trim().Count() > 0)
            {
                return true;
            }

            return false;
            
        }

        //TODO IsValid for other option types

        #endregion
    }
}