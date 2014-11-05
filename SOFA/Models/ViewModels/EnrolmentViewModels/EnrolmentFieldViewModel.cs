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

using SOFA.Models.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SOFA.Models.ViewModels.EnrolmentViewModels
{
    public class EnrolmentFieldViewModel : EnrolmentField, IValidatableObject
    {

        public EnrolmentFieldViewModel()
        {
            this.EnrollmentFieldOptions = new List<EnrolmentFieldOption>();
        }

        public EnrolmentFieldViewModel(EnrolmentField field) : base()
        {
            this.EnrolmentFieldId = field.EnrolmentFieldId;
            this.FieldType = field.FieldType;
            this.PromptValue = field.PromptValue;
            this.Value = field.Value;
            this.OriginalFieldId = field.OriginalFieldId;
            this.EnrollmentFieldOptions = field.EnrollmentFieldOptions;
        }

        public EnrolmentField toEnrolmentField()
        {
            var eField = new EnrolmentField()
            {
                EnrolmentFieldId = this.EnrolmentFieldId,
                PromptValue = this.PromptValue,
                Value = this.Value,
                FieldType = this.FieldType,
                OriginalFieldId = this.OriginalFieldId,
                EnrollmentFieldOptions = this.EnrollmentFieldOptions
            };

            return eField;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return EnrolmentValidator.ValidateField(this); 
        }
    }
}