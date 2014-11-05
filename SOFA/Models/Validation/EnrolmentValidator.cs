/*
 *  School Of Future Applications
 *
 *  Copyright (C) 2014  Terminal Coding
 *
 * This program is free software; you can redistribute it and/or
 * modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation; either version 2
 * of the License, or (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
 */

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
            if (possibleResponses.Count == 0 && String.IsNullOrWhiteSpace(field.Value))
                return true; //No responses set by staff
                
            List<String> trimmedPossResponses = new List<string>();
            foreach (var res in possibleResponses)
            {
                trimmedPossResponses.Add(res.Trim());
            }
            return trimmedPossResponses.Contains(field.Value.Trim());
        }

        #endregion

        

        #region Option Validation

        public static Boolean IsValidMandatory(EnrolmentField field)
        {
            return !String.IsNullOrWhiteSpace(field.Value);                      
        }

        public static Boolean IsValidNumeric(EnrolmentField field)
        {
            return String.IsNullOrWhiteSpace(field.Value) || Regex.IsMatch(field.Value, @"^\d[\d.]?$");
        }
        //TODO IsValid for other option types

        #endregion
    }
}