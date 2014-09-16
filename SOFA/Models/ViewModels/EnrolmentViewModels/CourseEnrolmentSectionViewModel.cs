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
        public int SelectedDepartment { get; set; }

        [Display(Name = "Select Department")]
        public SelectList DepartmentSelect { get; set; }

        
        public int SelectedCourse { get; set; }
        public SelectList CourseSelect { get; set; }

        public string SelectedYearLevel { get; set; }
        public SelectList YearLevelSelect { get; set; }

        [Required(ErrorMessage = "You must select a class")]
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