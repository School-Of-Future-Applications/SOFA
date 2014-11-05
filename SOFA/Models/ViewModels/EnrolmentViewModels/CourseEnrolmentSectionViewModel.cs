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

using SOFA.Models.Prefab;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SOFA.Models.ViewModels.EnrolmentViewModels
{
    public class CourseEnrolmentSectionViewModel : EnrolmentSectionViewModel
    {
        [Required]
        public int SelectedDepartment { get; set; }

        [Display(Name = "Department")]
        public SelectList DepartmentSelect { get; set; }

        [Required]
        public int SelectedCourse { get; set; }

        [Display(Name = "Course")]
        public SelectList CourseSelect { get; set; }

        [Required]
        public string SelectedYearLevel { get; set; }

        [Display(Name = "Year Level")]
        public SelectList YearLevelSelect { get; set; }

        [Required(ErrorMessage = "You must first select a class.")]
        public int SelectedClassId { get; set; }
                
        public CourseEnrolmentSectionViewModel(): base()
        { }

        public CourseEnrolmentSectionViewModel(EnrolmentSection section, List<Department> departments) : base(section)
        { 
            if (section.OriginalSectionId != PrefabSection.COURSE_SELECT)
            {
                throw new ArgumentOutOfRangeException("Enrolment Section not a course select section");
            }
            DepartmentSelect = new SelectList(departments, "Id", "DepartmentName");

            //At this stage just display the departments
            CourseSelect = new SelectList(Enumerable.Empty<SelectListItem>());
            YearLevelSelect = new SelectList(Enumerable.Empty<SelectListItem>());
        }

        




    }
}