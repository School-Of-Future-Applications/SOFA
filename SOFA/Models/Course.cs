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
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SOFA.Models
{
    public class Course
    {
        public const int COURSECODE_LENGTH = 30;
        public const int COURSENAME_LENGTH = 50;

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(Course.COURSECODE_LENGTH)]
        public String CourseCode { get; set; }

        [Required]
        [StringLength(Course.COURSENAME_LENGTH)]
        public String CourseName { get; set; }

        public virtual ICollection<ClassBase> ClassBases { get; set; }

        [Required]
        [DefaultValue(false)]
        public bool Deleted { get; set; }

        public virtual Department Department { get; set; }

        public Course()
        {
            ClassBases = new List<ClassBase>();
        }
    }
}