using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace SOFA
{
    public class Field : IValidatableObject
    {
        
        public const String TYPE_TEXT_FIELD = "TEXT_FIELD";
        public const String TYPE_TEXT_BOX = "TEXT_BOX";
        public const String TYPE_DATE = "DATE";
        public const String TYPE_FILE = "FILE";
        
        [Key]
        public String Id { get; set; }

        public string FieldType { get; set; }

        public string PromptValue { get; set; }

        public ICollection<FieldOption> FieldOptions;

        public IEnumerable<String> FieldTypes()
        {
            List<String> fieldTypes = new List<String>()
            {
                TYPE_TEXT_FIELD,
                TYPE_TEXT_BOX,
                TYPE_FILE,
                TYPE_DATE
            };
            
            return fieldTypes;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
