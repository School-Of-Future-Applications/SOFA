﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SOFA.Models
{
    public class Teacher
    {
        [Key]
        public int Id { get; set; }

        public String Name { get; set; }
    }
}