using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Collections.Generic;

namespace SOFA
{
    public class FormSection
    {
        public string formId;
        public string sectionId;
        public string belowOf;
        public ICollection<Section> Sections;
    }
}
