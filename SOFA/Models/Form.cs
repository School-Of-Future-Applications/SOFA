using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Collections.Generic;

namespace SOFA
{
    public class Form
    {
        public string formId;
        public string formName;
        public ICollection<FormSection> FormSections;
    }
}
