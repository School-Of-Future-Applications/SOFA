using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOFA.Models.ViewModels.FormViewModels
{
    public class FormSectionViewModel
    {
        public SectionViewModel Section { get; set; }

        public SectionViewModel BelowOf { get; set; }

        public FormSectionViewModel()
        {

        }

        public FormSectionViewModel(FormSection formsection) : this()
        {
            Section = new SectionViewModel(formsection.Section);
            BelowOf = new SectionViewModel(formsection.BelowOf);
        }

    }
}