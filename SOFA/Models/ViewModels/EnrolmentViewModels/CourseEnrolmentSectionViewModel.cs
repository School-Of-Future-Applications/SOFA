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