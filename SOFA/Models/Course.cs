using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SOFA.Models
{
    public class Course
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public String CourseCode { get; set; }

        [Required]
        public String CourseName { get; set; }

        public virtual ICollection<ClassBase> ClassBases { get; set; }

        public virtual Department Department { get; set; }
    }
}