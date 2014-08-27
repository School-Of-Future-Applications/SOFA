using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOFA.Models.ViewModels.Form
{
    public class FieldViewModel
    {
        public string FieldId { get; set; }

        public String FieldType { get; set; }

        public String PromptValue { get; set; }

        public String Value { get; set; }
    }
}