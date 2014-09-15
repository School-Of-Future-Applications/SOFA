﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

using SOFA.Infrastructure;
using SOFA.Models.Validation;

namespace SOFA.Models
{
    public class EnrolmentField 
    {
        [Key]
        public string EnrolmentFieldId { get; set; }
        public String FieldType { get; set; }
        public String PromptValue { get; set; }
        public virtual List<EnrolmentFieldOption> EnrollmentFieldOptions { get; set; }
        public String Value { get; set; }
        public String OriginalFieldId { get; set; }

        public EnrolmentField()
        {
            EnrolmentFieldId = UUIDUtil.NewUUID();
            EnrollmentFieldOptions = new List<EnrolmentFieldOption>();
        }

        public EnrolmentField(Field field)
            : this()
        {
            FieldType = field.FieldType;
            PromptValue = field.PromptValue;
            OriginalFieldId = field.Id;
            foreach (FieldOption opt in field.FieldOptions)
            {
                EnrollmentFieldOptions.Add(new EnrolmentFieldOption(opt));                
            }
        }

    }
}