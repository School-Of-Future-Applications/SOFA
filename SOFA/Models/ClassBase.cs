using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SOFA.Models
{
    public class ClassBase
    {
        public const int CLASSBASECODE_LENGTH = 30;
        public const int YEARLEVEL_LENGTH = 10;

        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(ClassBase.CLASSBASECODE_LENGTH)]
        public String ClassBaseCode { get; set; }

        [Required]
        [StringLength(ClassBase.YEARLEVEL_LENGTH)]
        public String YearLevel { get; set; }

        public virtual Course Course { get; set; }


        public virtual List<TimetabledClass> TimetabledClasses{ get; set; }

        public ClassBase()
        {
            this.TimetabledClasses = new List<TimetabledClass>();
        }
    }
}