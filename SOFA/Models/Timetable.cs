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
using SOFA.Models.Validation;

namespace SOFA.Models
{
    public class Timetable
    {
        [Key]
        public int Id { get; set; }

        [StringLength(20)]
        [Display(Name="Code")]
        public String TimetableCode { get; set; }

        [StringLength(255)]
        [Display(Name="name")]
        [Required]
        public String TimetableIdentifier { get; set; }

        [Display(Name="Active Date")]
        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime ActiveDate { get; set; }
        
        [Display(Name="Expiry Date")]
        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [AfterDate("ActiveDate")]
        public DateTime ExpiryDate { get; set; }

        public virtual ICollection<Line> Lines { get; set; }
    }
}