using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SOFA.Models
{
    public class EnrollmentFieldOption 
    {

        [Key]
        public int Id { get; set; }
        public String OptionType { get; set; }
        public String OptionValue { get; set; }
        public virtual EnrollmentField EnrollmentField { get; set; }

        public EnrollmentFieldOption()
        {

        }

        public EnrollmentFieldOption(FieldOption fieldOption) : this()
        {
            OptionType = fieldOption.OptionType;
            OptionValue = fieldOption.OptionValue;
        }
    }
}