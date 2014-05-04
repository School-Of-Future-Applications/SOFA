using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOFA.Models.ViewModels
{
    public class TimetabledClassCreateEditViewModel
    {

        public TimetabledClass TimetabledClass { get; set; }

        public int LineID { get; set; }
        public IEnumerable<ClassBase> ClassBases { get; set; }
    }
}