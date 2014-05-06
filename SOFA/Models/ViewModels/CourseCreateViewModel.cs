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
        [Display(Name="Course Name")]
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