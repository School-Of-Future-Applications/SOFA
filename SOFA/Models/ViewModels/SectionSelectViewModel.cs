using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SOFA.Models.ViewModels
{
    public class SectionSelectViewModel
    {
        public string SelectedSectionId { get; set; }

        public IEnumerable<SelectListItem> SectionItems { get; set; }


        public SectionSelectViewModel(List<Section> Sections)
        {
            SectionItems = new SelectList(Sections, "Id", "Name");
        }
    }
}