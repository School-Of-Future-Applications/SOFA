using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SOFA.Models
{
    public class Timetable
    {
        [Key]
        public int Id { get; set; }

        [Display(Name="Code")]
        public String TimetableCode { get; set; }

        [Display(Name="Name")]
        [Required]
        public String TimetableIdentifier { get; set; }
        
        [Display(Name="Active Date")]
        [Required]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime ActiveDate { get; set; }
        
        [Display(Name="Expiry Date")]
        [Required]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime ExpiryDate { get; set; }


        

        public virtual ICollection<Line> Lines { get; set; }
    }
}