using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

using SOFA.Infrastructure;

namespace SOFA.Models
{
    public class EnrolmentForm : IValidatableObject
    {
        public EnrolmentForm()
        {
            EnrolmentFormId = UUIDUtil.NewUUID();
            DateCreated = DateTime.Now;
            EnrolmentFormSections = new List<EnrolmentFormSection>();
        }

        public EnrolmentForm(Form form)
            : this()
        {
            fromForm(form);
        }

        [Key]
        public String EnrolmentFormId { get; set; }

        public String Name { get; set; }

        public DateTime DateCreated { get; set; }

        public Student Student { get; set; }

        public TimetabledClass Class { get; set; }

        public virtual ICollection<EnrolmentFormSection> EnrolmentFormSections { get; set; }

        private void fromForm(Form form)
        {
            Name = form.FormName;
            foreach (FormSection sec in form.FormSections)
                EnrolmentFormSections.Add(new EnrolmentFormSection(this, sec));
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return SOFA.Models.Validation.EnrolmentValidator.ValidateForm(this);
        }
    }
}