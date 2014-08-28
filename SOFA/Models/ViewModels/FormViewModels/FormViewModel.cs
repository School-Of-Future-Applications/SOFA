using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using SOFA.Models;

namespace SOFA.Models.ViewModels.FormViewModels
{
    public class FormViewModel
    {
        public string Id { get; set; }

        public string FormName { get; set; }

        public List<FormSectionViewModel> FormSections { get; set; }

        public FormViewModel()
        {
            FormSections = new List<FormSectionViewModel>();
        }

        public FormViewModel(Form form) : this()
        {
            Id = form.Id;
            FormName = form.FormName;
            foreach (var fs in form.FormSections)
            {
                FormSections.Add(new FormSectionViewModel(fs));
            }
        }

    }

}