using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

using SOFA.Infrastructure;

namespace SOFA
{
    public class Field : IValidatableObject
    {
        public Field()
        {
            Id = UUIDUtil.NewUUID();
        }

        [Key]
        public String Id { get; set; }

        public string FieldType { get; set; }

        public string PromptValue { get; set; }

        public ICollection<FieldOption> FieldOptions;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
