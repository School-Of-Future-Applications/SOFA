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
using SOFA.Models.Validation;

using SOFA.Infrastructure;

namespace SOFA.Models
{
    public class FieldOption : IValidatableObject
    {
        public const String OPT_MIN_VALUE = "MIN_VALUE";
        public const String OPT_MAX_VALUE = "MAX_VALUE";
        public const String OPT_MAX_LENGTH = "MAX_LENGTH";
        public const String OPT_NUMERIC = "NUMBER";
        public const String OPT_MANDATORY = "MANDATORY";
        public const String OPT_RESPONSE = "RESPONSE";

        /* Used to reference field id from enrolment field */
        public const String OPT_UUID = "UUID";

        public const String VAL_TRUE = "TRUE";
        public const String VAL_FALSE = "FALSE";

        [Key]
        public string FieldOptionId { get; set; }
        public string OptionType { get; set; }
        public string OptionValue { get; set; }
        public virtual Field Field { get; set; }

        public FieldOption()
        {
            FieldOptionId = UUIDUtil.NewUUID();
        }
        
        public FieldOption(String optionType) 
            : this()
        { 
            if (!FieldOptionTypes().Contains(optionType))
            {
                throw new ArgumentException("Invalid option type");
            } 
            else
            {
                OptionType = optionType;
            }
            //Set default values, if any
            if (optionType == OPT_MANDATORY || optionType == OPT_NUMERIC)
            {
                OptionValue = VAL_FALSE;
            }

        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return FormValidator.ValidateFieldOption(this);
        }

        public static ICollection<String> FieldOptionTypes()
        {
            List<String> optionTypes = new List<String>()
            {
                OPT_MIN_VALUE,
                OPT_MAX_VALUE,
                OPT_MAX_LENGTH,
                OPT_NUMERIC,
                OPT_MANDATORY,
                OPT_RESPONSE,
                OPT_UUID
            };

            return optionTypes;
        }
    }
}
