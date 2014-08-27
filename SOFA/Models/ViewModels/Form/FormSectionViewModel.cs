using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOFA.Models.ViewModels.Form
{
    public class FormSectionViewModel
    {
        public SectionViewModel Section { get; set; }

        public SectionViewModel BelowOf { get; set; }

    }
}