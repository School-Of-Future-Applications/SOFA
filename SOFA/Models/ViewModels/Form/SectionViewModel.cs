using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOFA.Models.ViewModels.Form
{
    public class SectionViewModel
    {
        public string SectionId { get; set; }

        public string SectionName { get; set; }

        public List<FieldViewModel> Fields { get; set; }

    }
}