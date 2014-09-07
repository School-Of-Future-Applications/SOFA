using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using SOFA.Infrastructure;
using SOFA.Models.Validation;

namespace SOFA.Models
{
    public class Form: IValidatableObject
    {
        public Form()
        {
            Id = UUIDUtil.NewUUID();
            FormSections = new List<FormSection>();
            DateCreated = DateTime.Now;
            LastModified = DateCreated;
        }

        public Form(string formName)
            : this()
        {
            FormName = formName;
        }

        [Key]
        public string Id { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

        public string DateCreatedString()
        {
            string date = "";
            if (DateCreated != null)
                date = DateCreated.ToString(DateTimeUtil.DATE_FORMAT);
            return date;
        }

        [Required]
        [StringLength(250)]
        [Display(Name = "Form Name")]
        public string FormName {get; set;}

        public virtual ICollection<FormSection> FormSections { get; set; }

        [Required]
        public DateTime LastModified { get; set; }

        public string LastModifiedString()
        {
            string date = "";
            if (LastModified != null)
                date = LastModified.ToString(DateTimeUtil.DATE_FORMAT);
            return date;
        }

        public void updateModified()
        {
            LastModified = DateTime.Now;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return FormValidator.ValidateForm(this);
        }
    }
}
