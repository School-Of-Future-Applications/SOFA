using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace SOFA
{
    public class FieldOption : IValidatableObject
    {

        public const String OPT_MIN_VALUE = "MIN_VALUE";
        public const String OPT_MAX_VALUE = "MAX_VALUE";
        public const String OPT_MAX_LENGTH = "MAX_LENGTH";
        public const String OPT_NUMERIC = "NUMBER";
        public const String OPT_MANDATORY = "MANDATORY";


        [Key]
        public string Id { get; set; }
        public string OptionType { get; set; }
        public string OptionValue { get; set; }
        public virtual Field Field { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
