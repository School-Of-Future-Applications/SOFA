using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace SOFA
{
    public class FormSection
    {
        public virtual Section Section { get; set; }

        public virtual Section BelowOf { get; set; }
    }
}
