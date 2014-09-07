using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SOFA.Models
{
    public class EnrolmentFormSection 
    {
        public EnrolmentFormSection()
        {
        }

        public EnrolmentFormSection(EnrolmentForm eForm, FormSection fSection
                                   ,Dictionary<string, EnrolmentSection> sectionTransform)
        {
            EnrolmentFormId = eForm.EnrolmentFormId;
            EnrolmentForm = eForm;
            fromFormSection(fSection, sectionTransform);
        }

        [Key, Column(Order = 1)]
        public String EnrolmentFormId { get; set; }

        [Key, Column(Order = 2)]
        public String EnrolmentSectionId { get; set; }

        public virtual EnrolmentForm EnrolmentForm { get; set; }

        public virtual EnrolmentSection EnrolmentSection { get; set; }

        public virtual EnrolmentSection BelowOf { get; set; }

       
        
        
        public static ICollection<EnrolmentFormSection> Sort(ICollection<EnrolmentSection> enrolmentSections)
        {
            List<EnrolmentFormSection> list = (List<EnrolmentFormSection>)enrolmentSections;
            //Find form section where below of == null. Put it at the top.
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].BelowOf == null)
                {
                    EnrolmentFormSection temp = list[0];
                    list[0] = list[i];
                    list[i] = temp;
                    break;
                }
            }

            for (int i = 1; i < list.Count - 1; i++)
            {
                for (int j = i; j < list.Count; j++)
                {
                    if (list[j].BelowOf == list[i - 1].EnrolmentSection)
                    {
                        EnrolmentFormSection temp = list[j];
                        list[j] = list[i];
                        list[i] = temp;
                        break;
                    }
                }
            }
            return list;
        }

        private void fromFormSection(FormSection fSection
                                    ,Dictionary<string, EnrolmentSection> sectionTransform)
        {
            EnrolmentSection newSection = sectionTransform[fSection.SectionId];
            EnrolmentSectionId = newSection.Id;
            EnrolmentSection = newSection;
            if (fSection.BelowOf != null)
                BelowOf = sectionTransform[fSection.BelowOf.Id];
        }
    }
}