using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOFA.Models
{
    public class SectionFieldOrder
    {
        public virtual Section Section { get; set;}
        public virtual Field Field { get; set; }
        public byte Order { get; set; }

    }
}