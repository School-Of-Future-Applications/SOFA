﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOFA.Models.ViewModels.FormViewModels
{
    public class FormSectionViewModel
    {
        public string formId { get; set; }

        public SectionViewModel Section { get; set; }

        public SectionViewModel BelowOf { get; set; }

        public FormSectionViewModel()
        {

        }

        public FormSectionViewModel(FormSection formsection) : this()
        {
            formId = formsection.FormId;
            Section = new SectionViewModel(formsection.Section);
            BelowOf = new SectionViewModel(formsection.BelowOf);
        }

    }
}