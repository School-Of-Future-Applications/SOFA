using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOFA.Models.ViewModels
{
    public class CreatePrerequisiteViewModel
    {
        public int classBaseId { get; set; }

        public Section Prerequisite { get; set; }
    }
}