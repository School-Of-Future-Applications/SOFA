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
