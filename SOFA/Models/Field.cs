using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace SOFA
{
    public class Field : IValidatableObject
    {
        [Key]
        public int ID { get; set; }

        public string FieldType { get; set; }

        public string PromptValue { get; set; }

        public ICollection<FieldOption> FieldOptions;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
