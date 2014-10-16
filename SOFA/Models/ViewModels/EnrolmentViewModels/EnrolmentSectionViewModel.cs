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