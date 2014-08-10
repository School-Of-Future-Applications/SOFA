using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace SOFA.Models
{
    public class FormSection
    {
        [Key, Column(Order = 1)]
        public String FormId { get; set; }

        [Key, Column(Order = 2)]
        public String SectionId { get; set; }

        public virtual Form Form { get; set; }

        public virtual Section Section { get; set; }

        public virtual Section BelowOf { get; set; }
    }
}
