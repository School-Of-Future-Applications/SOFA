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
    }
}