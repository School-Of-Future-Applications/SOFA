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

namespace SOFA.Models
{
    public class EnrolmentFieldOption 
    {

        [Key]
        public string EnrolmentFieldOptionId { get; set; }
        public String OptionType { get; set; }
        public String OptionValue { get; set; }
        public virtual EnrolmentField EnrollmentField { get; set; }

        public EnrolmentFieldOption()
        {
            EnrolmentFieldOptionId = UUIDUtil.NewUUID();
        }

        public EnrolmentFieldOption(FieldOption fieldOption)
            : this()
        {
            OptionType = fieldOption.OptionType;
            OptionValue = fieldOption.OptionValue;
        }
    }
}