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
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

using SOFA.Infrastructure;
using SOFA.Models.Validation;

namespace SOFA.Models
{
    public class Field : IValidatableObject
    {   
        public const String TYPE_TEXT_MULTI = "TEXT_MULTI";
        public const String TYPE_TEXT_SINGLE = "TEXT_SINGLE";
        public const String TYPE_DATE = "DATE";
        public const String TYPE_FILE = "FILE";
        public const String TYPE_DROPDOWN = "DROPDOWN";
        public const String TYPE_INFO = "INFORMATION";

        public readonly String[] TEXT_MULTI_OPTIONS = { FieldOption.OPT_MANDATORY };
        public readonly String[] TEXT_SINGLE_OPTIONS = { FieldOption.OPT_MANDATORY, FieldOption.OPT_NUMERIC };
        public readonly String[] FILE_OPTIONS = { FieldOption.OPT_MANDATORY };
        public readonly String[] DATE_OPTIONS = { FieldOption.OPT_MANDATORY };
        public readonly String[] DROPDOWN_OPTIONS = { FieldOption.OPT_MANDATORY, FieldOption.OPT_RESPONSE };

        public static List<String> FIELD_TYPES = new List<String>()
            {
                 TYPE_TEXT_MULTI
                ,TYPE_TEXT_SINGLE
                ,TYPE_DATE
                ,TYPE_FILE
                ,TYPE_DROPDOWN
                ,TYPE_INFO
            };

        [Key]
        public String Id { get; set; }

        public string FieldType { get; set; }

        public string PromptValue { get; set; }

        public virtual ICollection<FieldOption> FieldOptions { get; set; }

        public virtual Section Section { get; set; }

        public Field()
        {
            Id = UUIDUtil.NewUUID();
            FieldOptions = new List<FieldOption>();
        }

        public Field(String fieldType) : this()
        {
            if (!FIELD_TYPES.Contains(fieldType))
            {
                throw new ArgumentException("Invalid field type");
            }
            else
            {
                FieldType = fieldType;
            }
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return FormValidator.ValidateField(this);
        }
    }
}
