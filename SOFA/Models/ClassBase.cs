using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SOFA.Models
{
    public class ClassBase
    {
        [Key]
        public int Id { get; set; }


        public String ClassBaseCode { get; set; }

        public String YearLevel { get; set; }

        public virtual Course Course { get; set; }


        public virtual List<TimetabledClass> TimetabledClasses{ get; set; }

        public ClassBase()
        {
            this.TimetabledClasses = new List<TimetabledClass>();
        }
    }
}