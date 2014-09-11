using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOFA.Models.Prefab.Fields
{
    public class FirstName : PrefabField
    {
        private const string PROMPT = "First Name";
        private const string FIELDTYPE = Field.TYPE_TEXT_SINGLE;

        private Field field;

        public FirstName(string id) : base(id)
        {
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

    public class LastName : PrefabField
    {
        private const string PROMPT = "Last Name";
        private const string FIELDTYPE = Field.TYPE_TEXT_SINGLE;
        
        public LastName(string id) : base(id)
        {

        }

        private Field field;

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