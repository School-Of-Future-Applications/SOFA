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
        public String TimetableIdentifier { get; set; }

        [Display(Name="Expiry Date")]
        public DateTime ExpiryDate { get; set; }

        [Display(Name="Active Date")]
        public DateTime ActiveDate { get; set; }

        public virtual ICollection<Line> Lines { get; set; }
    }
}