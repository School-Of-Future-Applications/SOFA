using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Collections.Generic;

namespace SOFA
{
    public class Section
    {
        public string sectionId;
        public DateTime dateCreated;
        public string name;
        public ICollection<Field> Fields;
    }
}
