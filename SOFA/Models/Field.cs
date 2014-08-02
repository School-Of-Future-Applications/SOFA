using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Collections.Generic;

namespace SOFA
{
    public class Field
    {
        public int id;
        public string type;
        public string prompt_value;
        public ICollection<FieldOptions> FieldOptions;
    }
}
