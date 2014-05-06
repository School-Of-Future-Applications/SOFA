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