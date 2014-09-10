using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOFA.Models.ViewModels.FormViewModels
{
    public class SectionViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public List<FieldViewModel> Fields { get; set; }

        public SectionViewModel()
        {
            Fields = new List<FieldViewModel>();
        }

        public SectionViewModel(Section section) : this()
        {
            if (section != null)
            {
                Id = section.Id;
                Name = section.Name;
                foreach (var f in section.Fields)
                {
                    var ef = new EnrolmentField(f);
                    Fields.Add(new FieldViewModel(ef));
                }
            }

        }

    }
}