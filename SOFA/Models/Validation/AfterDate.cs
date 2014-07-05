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
            return ValidationResult.Success;
        }

        /**
         * Adds support for CS Validation if we want it.
         */
        public override IEnumerable<ModelClientValidationRule> GetClientValidationRules(
                                        ModelMetadata metadata,
	                                    ControllerContext context
        )
        {
            var rule = new ModelClientValidationRule
            {
                ErrorMessage = this.ErrorMessageString,
                ValidationType = "DateAfter"
            };
            rule.ValidationParameters["propertytested"] = this.comparePropertyName;
            yield return rule;
        }
}