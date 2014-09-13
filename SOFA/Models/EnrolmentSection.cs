using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

using SOFA.Infrastructure;

namespace SOFA.Models
{
    public class EnrolmentSection
    {
        public EnrolmentSection()
        {
            Id = UUIDUtil.NewUUID();
            EnrolmentFields = new List<EnrolmentField>();
            DateCreated = DateTime.Now;
        }

        public EnrolmentSection(Section section)
            : this()
        {
            fromSection(section);
        }

        [Key]
        public string Id { get; set; }

        public DateTime DateCreated { get; set; }

        public String SectionName { get; set; }

        public virtual ICollection<EnrolmentField> EnrolmentFields { get; set; }

        //public virtual ICollection<EnrolmentFormSection> EnrolmentFormSections { get; set; }

        private void fromSection(Section section)
        {
            SectionName = section.Name;
            DateCreated = DateTime.Now;
            foreach (Field f in section.Fields)
                EnrolmentFields.Add(new EnrolmentField(f));
        }
    }
}