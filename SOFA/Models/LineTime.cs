using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SOFA.Models
{
    public class LineTime
    {
        [Key]
        public int Id { get; set; }           

        public Int32 Day { get; set; }

        public Double StartTime { get; set; }

        public Double EndTime { get; set; }
        
        [NotMapped]
        public String StartTimeString { get; set; }
        [NotMapped]
        public String EndTimeString { get; set; }

        public virtual Line Line { get; set; }
    }
}