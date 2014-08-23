using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SOFA.Models
{
    public class EnrolmentFieldOption 
    {

        [Key]
        public int Id { get; set; }
        public String OptionType { get; set; }
        public String OptionValue { get; set; }
        public virtual EnrolmentField EnrollmentField { get; set; }

        public EnrolmentFieldOption()
        {

        }

        public EnrolmentFieldOption(FieldOption fieldOption) : this()
        {
            OptionType = fieldOption.OptionType;
            OptionValue = fieldOption.OptionValue;
        }
    }
}