using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using SOFA.Infrastructure;

namespace SOFA.Models
{
    public class Form: IValidatableObject
    {
        public Form()
        {
            Id = UUIDUtil.NewUUID();
            FormSections = new List<FormSection>();
        }

        [Key]
        public string Id { get; set; }

        public string FormName {get; set;}

        public virtual ICollection<FormSection> FormSections { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            yield return null;
        }
    }
}
