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

        public String OriginalSectionId { get; set; }

        public virtual ICollection<EnrolmentField> EnrolmentFields { get; set; }

        //public virtual ICollection<EnrolmentFormSection> EnrolmentFormSections { get; set; }

        private void fromSection(Section section)
        {
            SectionName = section.Name;
            DateCreated = DateTime.Now;
            OriginalSectionId = section.Id;
            foreach (Field f in section.Fields)
                EnrolmentFields.Add(new EnrolmentField(f));
        }
    }
}