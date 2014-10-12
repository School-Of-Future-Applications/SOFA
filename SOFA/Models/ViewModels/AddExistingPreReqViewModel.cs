using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SOFA.Models.ViewModels.FormViewModels;

namespace SOFA.Models.ViewModels
{
    public class AddExistingPreReqViewModel : SectionSelectViewModel
    {
        public int ClassBaseId { get; set; }

        public AddExistingPreReqViewModel(int ClassBaseId, List<Section> sections) : base(sections)
        {
            this.ClassBaseId = ClassBaseId;
        }
    }
}