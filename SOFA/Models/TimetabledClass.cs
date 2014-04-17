using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SOFA.Models
{
    public class TimetabledClass
    {

        [Key]
        public int Id { get; set; }

        public ClassBase ClassBase { get; set; }

        public Line Line { get; set; }

        public Int32 Capacity { get; set; }

        public Teacher Teacher { get; set; }
    }
}