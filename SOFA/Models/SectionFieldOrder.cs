using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SOFA.Models
{
    public class SectionFieldOrder
    {
        [Key, ForeignKey("Section"), Column(Order=0)]
        public string SectionID { get; set; }
        [Key, ForeignKey("Field"), Column(Order = 1)]
        public string FieldID { get; set; }
        public virtual Section Section { get; set;}
        public virtual Field Field { get; set; }
        public int Order { get; set; }

    }
}