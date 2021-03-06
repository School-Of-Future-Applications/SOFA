﻿/*
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
using SOFA.Models.Validation;

namespace SOFA.Models
{
    public class EnrolmentField 
    {
        [Key]
        public string EnrolmentFieldId { get; set; }

        [Required]
        public String FieldType { get; set; }

        [Required]
        public String PromptValue { get; set; }

        public virtual List<EnrolmentFieldOption> EnrollmentFieldOptions { get; set; }

        public String Value { get; set; }

        public String OriginalFieldId { get; set; }

        public EnrolmentField()
        {
            EnrolmentFieldId = UUIDUtil.NewUUID();
            EnrollmentFieldOptions = new List<EnrolmentFieldOption>();
        }

        public EnrolmentField(Field field)
            : this()
        {
            FieldType = field.FieldType;
            PromptValue = field.PromptValue;
            OriginalFieldId = field.Id;
            foreach (FieldOption opt in field.FieldOptions)
            {
                EnrollmentFieldOptions.Add(new EnrolmentFieldOption(opt));                
            }
        }

    }
}