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
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SOFA.Models
{
    public class LineTime
    {
        [Key]
        public int Id { get; set; }           

        public Int32 Day { get; set; }

        [Range(0d,25d)]
        public Double StartTime { get; set; }

        [Range(0d, 25d)]
        public Double EndTime { get; set; }
        
        [NotMapped]
        [RegularExpression(@"^([0-9]|0[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$",ErrorMessage="Not a valid time")]
        [Display(Name = "Start Time")]
        public String StartTimeString { get; set; }

        [NotMapped]
        [RegularExpression(@"^([0-9]|0[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$", ErrorMessage = "Not a valid time")]
        [Display(Name = "End Time")]
        public String EndTimeString { get; set; }

        public virtual Line Line { get; set; }
    }
}