using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using SOFA.Models;
using System.Web.Routing;

namespace SOFA.Models.ViewModels
{
    public class EnrolmentFormViewModel
    {
        public String ApproveAction { get; set; }
        public Dictionary<String, String> ApproveArgs { get; set; }
        public String ApproveController { get; set; }
        public String DeleteAction { get; set; }
        public Dictionary<String, String> DeleteArgs { get; set; }
        public String DeleteController { get; set; }
        public EnrolmentForm EnrolmentForm { get; set; }

        public EnrolmentFormViewModel()
        {
            this.ApproveArgs = new Dictionary<String, String>();
            this.DeleteArgs = new Dictionary<String, String>();
        }
    }
}