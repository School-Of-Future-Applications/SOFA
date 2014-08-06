using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

using SOFA.Infrastructure;

namespace SOFA
{
    public class Field : IValidatableObject
    {

        
        public const String TYPE_TEXT_FIELD = "TEXT_FIELD";
        public const String TYPE_TEXT_BOX = "TEXT_BOX";
        public const String TYPE_DATE = "DATE";
        public const String TYPE_FILE = "FILE";
        public const String TYPE_DROPDOWN = "DROPDOWN";
        public const String TYPE_INFO = "INFORMATION";
        

        public Field()
        {
            Id = UUIDUtil.NewUUID();
        }


        [Key]
        public String Id { get; set; }
        
        public string FieldType { get; set; }

        public string PromptValue { get; set; }

        public virtual ICollection<FieldOption> FieldOptions { get; set; }

        public static IEnumerable<String> FieldTypes()
        {
            List<String> fieldTypes = new List<String>()
            {
                TYPE_TEXT_FIELD,
                TYPE_TEXT_BOX,
                TYPE_FILE,
                TYPE_DATE,
                TYPE_DROPDOWN,
                TYPE_INFO
            };
            
            return fieldTypes;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
