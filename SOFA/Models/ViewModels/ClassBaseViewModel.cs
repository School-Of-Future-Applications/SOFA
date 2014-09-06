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

namespace SOFA.Models.ViewModels
{
    public class ClassBaseViewModel
    {
        public int Id { get; set; }

        public int CourseID { get; set; }

        public String CourseName { get; set; }

        [Required]
        [Display(Name="Class Base Code")]
        [StringLength(ClassBase.CLASSBASECODE_LENGTH)]
        public String ClassBaseCode { get; set; }

        public int DepartmentId { get; set; }

        public String DepartmentName { get; set; }

        [Required]
        [Display(Name="Year Level")]
        [StringLength(ClassBase.YEARLEVEL_LENGTH)]
        public String YearLevel { get; set; }

        public ClassBase ToClassBase(Course course)
        {
            ClassBase classBase = new ClassBase();
            classBase.Course = course;
            classBase.Id = this.Id;
            classBase.ClassBaseCode = this.ClassBaseCode;
            classBase.YearLevel = this.YearLevel;

            return classBase;
        }

        public ClassBaseViewModel()
        {
        }

        public ClassBaseViewModel(ClassBase classBase)
        {
            Id = classBase.Id;
            CourseID = classBase.Course.Id;
            CourseName = classBase.Course.CourseName;
            ClassBaseCode = classBase.ClassBaseCode;
            DepartmentId = classBase.Course.Department.id;
            DepartmentName = classBase.Course.Department.DepartmentName;
            YearLevel = classBase.YearLevel;
        }

        public ClassBaseViewModel(Course course)
        {
            CourseID = course.Id;
            CourseName = course.CourseName;
            DepartmentId = course.Department.id;
            DepartmentName = course.Department.DepartmentName;
        }
    }
}