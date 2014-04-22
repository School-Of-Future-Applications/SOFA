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

        [Display(Name="Class Base Code")]
        public String ClassBaseCode { get; set; }

        [Required]
        [Display(Name="Year Level")]
        public String YearLevel { get; set; }
    }
}