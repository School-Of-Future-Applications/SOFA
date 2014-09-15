using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using SOFA.Models.Validation;

using SOFA.Infrastructure;

namespace SOFA.Models
{
    public class FieldOption : IValidatableObject
    {
        public const String OPT_MIN_VALUE = "MIN_VALUE";
        public const String OPT_MAX_VALUE = "MAX_VALUE";
        public const String OPT_MAX_LENGTH = "MAX_LENGTH";
        public const String OPT_NUMERIC = "NUMBER";
        public const String OPT_MANDATORY = "MANDATORY";
        public const String OPT_RESPONSE = "RESPONSE";

        /* Used to reference field id from enrolment field */
        public const String OPT_UUID = "UUID";

        public const String VAL_TRUE = "TRUE";

        [Key]
        public string FieldOptionId { get; set; }
        public string OptionType { get; set; }
        public string OptionValue { get; set; }
        public virtual Field Field { get; set; }

        public FieldOption()
        {
            FieldOptionId = UUIDUtil.NewUUID();
        }
        
        public FieldOption(String optionType) 
            : this()
        { 
            if (!FieldOptionTypes().Contains(optionType))
            {
                throw new ArgumentException("Invalid option type");
            } 
            else
            {
                OptionType = optionType;
            }
            //Set default values, if any
            if (optionType == OPT_MANDATORY || optionType == OPT_NUMERIC)
            {
                OptionValue = VAL_TRUE;
            }

        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return FormValidator.ValidateFieldOption(this);
        }

        public static ICollection<String> FieldOptionTypes()
        {
            List<String> optionTypes = new List<String>()
            {
                OPT_MIN_VALUE,
                OPT_MAX_VALUE,
                OPT_MAX_LENGTH,
                OPT_NUMERIC,
                OPT_MANDATORY,
                OPT_RESPONSE,
                OPT_UUID
            };

            return optionTypes;
        }
    }
}
