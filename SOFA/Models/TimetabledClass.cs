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

        public virtual ClassBase ClassBase { get; set; }

        
        public int? ClassBaseID { get; set; }
        public virtual Line Line { get; set; }

        public Int32 Capacity { get; set; }

        public virtual Teacher Teacher { get; set; }
    }
}