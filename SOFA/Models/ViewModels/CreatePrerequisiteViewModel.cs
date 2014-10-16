using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOFA.Models.ViewModels
{
    public class CreatePrerequisiteViewModel
    {
        public int ClassBaseId { get; set; }

        public Section Prerequisite { get; set; }
    
        
        public CreatePrerequisiteViewModel()
        {
            Prerequisite = new Section();
        }
    }
}