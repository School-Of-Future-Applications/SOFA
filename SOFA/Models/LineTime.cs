using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SOFA.Models
{
    public class LineTime
    {
        [Key]
        public int Id { get; set; }           

        public Int32 Day { get; set; }

        public Double Time { get; set; }

        public virtual Line Line { get; set; }
    }
}