using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SOFA
{
    public class Form: IValidatableObject
    {
        [Key]
        public string Id { get; set; }

        public string FormName {get; set;}

        public ICollection<FormSection> FormSections { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
