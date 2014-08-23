using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

using SOFA.Infrastructure;
using SOFA.Models.Validation;

namespace SOFA.Models
{
    public class Field : IValidatableObject
    {   
        public const String TYPE_TEXT_FIELD = "TEXT_FIELD";
        public const String TYPE_TEXT_BOX = "TEXT_BOX";
        public const String TYPE_DATE = "DATE";
        public const String TYPE_FILE = "FILE";
        public const String TYPE_DROPDOWN = "DROPDOWN";
        public const String TYPE_INFO = "INFORMATION";

        public static List<String> FIELD_TYPES = new List<String>()
            {
                 TYPE_TEXT_FIELD
                ,TYPE_TEXT_BOX
                ,TYPE_DATE
                ,TYPE_FILE
                ,TYPE_DROPDOWN
                ,TYPE_INFO
            };

        [Key]
        public String Id { get; set; }

        public string FieldType { get; set; }

        public string PromptValue { get; set; }

        public virtual ICollection<FieldOption> FieldOptions { get; set; }

        public virtual Section Section { get; set; }

        public Field()
        {
            Id = UUIDUtil.NewUUID();
            FieldOptions = new List<FieldOption>();
        }

        public Field(String fieldType) : this()
        {
            if (!FIELD_TYPES.Contains(fieldType))
            {
                throw new ArgumentException("Invalid field type");
            }
            else
            {
                FieldType = fieldType;
            }
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return new List<ValidationResult>();
        }
    }
}
