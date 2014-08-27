using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOFA.Models.ViewModels.FormViewModels
{
    public class FieldViewModel
    {
        public string FieldId { get; set; }

        public String FieldType { get; set; }

        public String PromptValue { get; set; }

        public String Value { get; set; }

        public FieldViewModel()
        {

        }

        public FieldViewModel(EnrolmentField field) : this()
        {
            FieldId = field.EnrolmentFIeldId;
            FieldType = field.FieldType;
            PromptValue = field.PromptValue;
            Value = field.Value;
        }
    }
}