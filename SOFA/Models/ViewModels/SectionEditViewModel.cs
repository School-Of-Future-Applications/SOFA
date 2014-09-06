using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOFA.Models.ViewModels
{
    public class SectionEditViewModel
    {
        public virtual Section Section { get; set; }
        public ICollection<Field> OrderedFields { get; set; }
    }
}