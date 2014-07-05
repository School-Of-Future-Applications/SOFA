using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
            //Get the type of the compared property.
            var comparePropInfo = validationContext.ObjectType.GetProperty(this.comparePropertyName);
            if (comparePropInfo == null)
            {
                return new ValidationResult(String.Format("Invalid property: {0}", this.comparePropertyName));
            }
            //Get the actual value of the compared property.
            var comparePropValue = comparePropInfo.GetValue(validationContext.ObjectInstance);

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
            
            return new ValidationResult(String.Format("{0} must be greater than {1}", 
                                            validationContext.DisplayName,
                                            comparePropertyName));

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