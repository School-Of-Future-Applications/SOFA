using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SOFA.Models.ViewModels.EnrolmentViewModels
{
    public class EnrolmentSectionViewModel : IValidatableObject
    {
        public string SectionId { get; set; }

        public string FormId { get; set; }

        public DateTime DateCreated { get; set; }

        public string SectionName { get; set; }

        public int SectionNumber { get; set; }

        public int TotalSections { get; set; }

        public ICollection<EnrolmentField> EnrolmentFields { get; set; }

        /* Constructors */
        public EnrolmentSectionViewModel()
        {

        }

        public EnrolmentSectionViewModel(EnrolmentSection section) : this()
        {
            SectionId = section.Id;
            DateCreated = section.DateCreated;
            EnrolmentFields = section.EnrolmentFields;
            SectionName = section.SectionName;
        }
        
        /* Conversion method */
        public EnrolmentSection toEnrolmentSection()
        {
            var enrolSection = new EnrolmentSection()
            {
                Id = this.SectionId,
                SectionName = this.SectionName,
                DateCreated = this.DateCreated,
                EnrolmentFields = this.EnrolmentFields
            };
            return enrolSection;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}