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
        public enum EnrolmentStatus
        {
             Pending = 1     /* New enrolment forms that have not been 100% filled out */
            ,Completed = 2   /* Enrolment forms that have been 100% filled out */
            ,Approved = 3    /* Enrolment forms that have been improved once completed */
            ,Disapproved = 4 /* Enrolment forms that have been disapproved */
        }

        public EnrolmentForm()
        {
            EnrolmentFormId = UUIDUtil.NewUUID();
            DateCreated = DateTime.Now;
            EnrolmentFormSections = new List<EnrolmentFormSection>();
            //Student = new Student();
            Status = EnrolmentStatus.Pending;
            Class = new TimetabledClass();
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

        public virtual Student Student { get; set; }

        [Required]
        public EnrolmentStatus Status { get; set; }

        public virtual TimetabledClass Class { get; set; }

        public virtual ICollection<EnrolmentFormSection> EnrolmentFormSections { get; set; }

        private void fromForm(Form form)
        {
            Dictionary<string, EnrolmentSection> sectionTransform = new Dictionary<string, EnrolmentSection>();
            Name = form.FormName;

            foreach (FormSection sec in form.FormSections)
                sectionTransform.Add(sec.SectionId, new EnrolmentSection(sec.Section));

            foreach (FormSection sec in form.FormSections)
                EnrolmentFormSections.Add(new EnrolmentFormSection(this, sec, sectionTransform));
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return SOFA.Models.Validation.EnrolmentValidator.ValidateForm(this);
        }
    }
}