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
        [Display(Name="Name")]
        public string CourseName { get; set; }
        
        [Required]
        [Display(Name="Code")]
        public string CourseCode { get; set; }
        
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
        }
        
        

    }
}