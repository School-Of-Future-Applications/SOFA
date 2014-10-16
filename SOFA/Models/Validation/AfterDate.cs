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
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SOFA.Models.Validation
{
    /**
     * Custom validate attribute to compare dates
     */
    public sealed class AfterDate : ValidationAttribute, IClientValidatable
    {
        private readonly string comparePropertyName;

        public AfterDate(string comparePropertyName)
        {
            this.comparePropertyName = comparePropertyName;
        }

        /**
         * Returns a successful validation if the value of the DateTime variable
         * upon which this is invoked is after the DateTime value of compareProperty.
         */
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //Get the type info of the compared property.
            var comparePropInfo = validationContext.ObjectType.GetProperty(this.comparePropertyName);
            if (comparePropInfo == null)
            {
                return new ValidationResult(String.Format("Invalid property: {0}", this.comparePropertyName));
            }
            //Get the actual value of the compared property.
            var comparePropValue = comparePropInfo.GetValue(validationContext.ObjectInstance);
            //Begin comparison.
            //Cover null cases and type mismatches first.
            if (value == null || !(value is DateTime) ||
                comparePropValue == null || !(comparePropValue is DateTime))
            {
                return ValidationResult.Success;
            }
            //Do the actual comparison.
            if ((DateTime)value > (DateTime)comparePropValue)
            {
                return ValidationResult.Success;
            }
            //Get the display name and use it for the error message.
            //If it doesn't exist then use the property name.
            var displayAttribute = (DisplayAttribute) comparePropInfo.GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault();
            String comparePropDispName;
            if (displayAttribute == null)
            {
                comparePropDispName = comparePropertyName;
            }
            else
            {
                comparePropDispName = displayAttribute.Name;
            }
            return new ValidationResult(String.Format("{0} must be after {1}", 
                                            validationContext.DisplayName,
                                            comparePropDispName));

        }

        /**
         * Adds support for CS Validation if we want it later.
         */
        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(
                                        ModelMetadata metadata,
                                        ControllerContext context
        )
        {
            var rule = new ModelClientValidationRule
            {
                ErrorMessage = this.ErrorMessageString,
                ValidationType = "dateafter"
            };
            rule.ValidationParameters["propertytested"] = this.comparePropertyName;
            yield return rule;
        }
    }
}