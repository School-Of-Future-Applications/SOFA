﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

using SOFA.Infrastructure;

namespace SOFA.Models
{
    public class EnrolmentField : IValidatableObject
    {
        [Key]
        public string EnrolmentFIeldId { get; set; }
        public String FieldType { get; set; }
        public String PromptValue { get; set; }
        public virtual List<EnrolmentFieldOption> EnrollmentFieldOptions { get; set; }
        public String Value { get; set; }

        public EnrolmentField()
        {
            EnrolmentFIeldId = UUIDUtil.NewUUID();
            EnrollmentFieldOptions = new List<EnrolmentFieldOption>();
        }

        public EnrolmentField(Field field)
            : this()
        {
            FieldType = field.FieldType;
            PromptValue = field.PromptValue;

            foreach (FieldOption opt in field.FieldOptions)
            {
                EnrollmentFieldOptions.Add(new EnrolmentFieldOption(opt));                
            }
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return null;
            throw new NotImplementedException();
        }
    }
}