using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SOFA.Models.ViewModels.Form
{
    public class FormViewModel
    {
        public string FormId { get; set; }

        public string FormName { get; set; }

        public List<FormSectionViewModel> FormSections { get; set; }

    }
}