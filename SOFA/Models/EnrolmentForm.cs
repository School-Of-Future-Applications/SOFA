/*
 *  School Of Future Applications
 *
 *  Copyright (C) 2014  Terminal Coding
 *
 * This program is free software; you can redistribute it and/or
 * modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation; either version 2
 * of the License, or (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

using SOFA.Infrastructure;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

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
            Status = EnrolmentStatus.Pending;
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