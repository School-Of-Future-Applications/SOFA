using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOFA.Models.Prefab.CourseSelect
{
    public class CourseSelectField : PrefabField
    {
        private const string PROMPT = "Class";
        private const string FIELDTYPE = Field.TYPE_TEXT_SINGLE;

        private Field field;

        public CourseSelectField()
        {
            this.Id = PrefabField.CLASS_SELECT;
            this.Prompt = PROMPT;
        }

        public override Field GetField()
        {
            if (this.field == null)
            {
                this.field = new Field();
                this.field.Id = this.GetId();
                this.field.PromptValue = this.GetPromptValue();
                this.field.FieldType = FIELDTYPE;
                this.field.FieldOptions.Add(new FieldOption(FieldOption.OPT_MANDATORY));
            }

            return this.field;
        }
    }
}