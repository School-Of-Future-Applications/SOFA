using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

using SOFA.Infrastructure;

namespace SOFA.Models
{
    public class EnrolmentFieldOption 
    {

        [Key]
        public string EnrolmentFieldOptionId { get; set; }
        public String OptionType { get; set; }
        public String OptionValue { get; set; }
        public virtual EnrolmentField EnrollmentField { get; set; }

        public EnrolmentFieldOption()
        {
            EnrolmentFieldOptionId = UUIDUtil.NewUUID();
        }

        public EnrolmentFieldOption(FieldOption fieldOption)
            : this()
        {
            OptionType = fieldOption.OptionType;
            OptionValue = fieldOption.OptionValue;
        }
    }
}