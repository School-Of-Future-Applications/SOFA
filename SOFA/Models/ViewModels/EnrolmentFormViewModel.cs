using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using SOFA.Models;

namespace SOFA.Models.ViewModels
{
    public class EnrolmentFormViewModel
    {
        public String ApproveAction { get; set; }
        public object ApproveArgs { get; set; }
        public String ApproveController { get; set; }
        public String DeleteAction { get; set; }
        public Object DeleteArgs { get; set; }
        public String DeleteController { get; set; }
        public EnrolmentForm EnrolmentForm { get; set; }
    }
}