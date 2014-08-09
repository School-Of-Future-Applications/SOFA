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
using SOFA.Models;

namespace SOFA.Models.ViewModels
{
    public class CourseCreateViewModel
    {
        [Required]
        public int ID { get; set; }

        [Required]
        [Display(Name = "Course Code")]
        [StringLength(Course.COURSECODE_LENGTH)]
        public string CourseCode { get; set; }

        [Required]
        [Display(Name="Course name")]
        [StringLength(Course.COURSENAME_LENGTH)]
        public string CourseName { get; set; }
        
        [UIHint("Hidden")]
        [Required]
        public int DepartmentId { get; set; }

        [Display(AutoGenerateField=false)]
        public String DepartmentName { get; set; }

        public CourseCreateViewModel()
        {

        }
        
        public CourseCreateViewModel(Course course)
        {
            this.CourseCode = course.CourseCode;
            this.CourseName = course.CourseName;
            this.ID = course.Id;
        }
  
        

    }
}