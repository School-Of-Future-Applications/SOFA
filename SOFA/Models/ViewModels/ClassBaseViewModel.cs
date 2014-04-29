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

        [Display(Name="Class Base Code")]
        public String ClassBaseCode { get; set; }

        [Required]
        [Display(Name="Year Level")]
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
    }
}