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
