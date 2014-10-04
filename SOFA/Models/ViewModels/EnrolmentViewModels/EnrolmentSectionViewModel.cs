 using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SOFA.Models.ViewModels.EnrolmentViewModels
{
    public class EnrolmentSectionViewModel 
    {
        public string SectionId { get; set; }

        public string OriginalSectionId { get; set; }

        public string FormId { get; set; }

        public DateTime DateCreated { get; set; }

        public string SectionName { get; set; }

        public int SectionNumber { get; set; }

        public int TotalSections { get; set; }

        public ICollection<EnrolmentFieldViewModel> EnrolmentFields { get; set; }

        /* Constructors */
        public EnrolmentSectionViewModel()
        {
            EnrolmentFields = new List<EnrolmentFieldViewModel>();
        }

        public EnrolmentSectionViewModel(EnrolmentSection section) : this()
        {
            SectionId = section.Id;
            DateCreated = section.DateCreated;
            SectionName = section.SectionName;
            OriginalSectionId = section.OriginalSectionId;
            foreach (var field in section.EnrolmentFields)
            {
                EnrolmentFields.Add(new EnrolmentFieldViewModel(field));
            }
        }
        
        /* Conversion method */
        public virtual EnrolmentSection toEnrolmentSection()
        {
            var enrolSection = new EnrolmentSection()
            {
                Id = this.SectionId,
                SectionName = this.SectionName,
                DateCreated = this.DateCreated,
                OriginalSectionId = this.OriginalSectionId
            };
            var eFields = new List<EnrolmentField>();
            foreach (var efvm in this.EnrolmentFields)
            {
                eFields.Add(efvm.toEnrolmentField());
            }
            enrolSection.EnrolmentFields = eFields;
            return enrolSection;
        }


    }
}