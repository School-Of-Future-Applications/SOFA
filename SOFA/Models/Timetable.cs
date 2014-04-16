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

        public String TimetableCode { get; set; }

        public String TimetableIdentifier { get; set; }

        public DateTime ExpiryDate { get; set; }

        public DateTime ActiveDate { get; set; }

        public virtual ICollection<Line> Lines { get; set; }
    }
}