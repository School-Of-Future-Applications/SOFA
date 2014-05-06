﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SOFA.Models
{
    public class Department
    {
        [Key]
        public int id { get; set; }

        [Required]
        [DefaultValue(false)]
        public bool Deleted { get; set; }

        [Required]
        [StringLength(50)]
        public String DepartmentName { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
    }
}