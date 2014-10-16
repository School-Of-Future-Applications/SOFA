using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace SOFA.Models.ViewModels
{
    public class SectionSelectViewModel
    {
        public string SelectedSectionId { get; set; }
        
        [Display(Name="Section")]
        public IEnumerable<SelectListItem> SectionItems { get; set; }

        
        public SectionSelectViewModel()
        { }

        public SectionSelectViewModel(List<Section> Sections) : this()
        {
            SectionItems = new SelectList(Sections, "Id", "Name");
        }
    }
}