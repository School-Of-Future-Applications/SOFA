using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOFA.Models.Prefab.StandardSection
{
   public class TestField_A : PrefabField
   {
       private const string PROMPT = "Test Field A";
       private const string FIELDTYPE = Field.TYPE_TEXT_SINGLE;

       private Field field;

       public TestField_A()
       {
           this.Id = PrefabField.TESTFIELD_A;
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

    public class TestDateField_A : PrefabField
    {
        private const string PROMPT = "Test Date field";
        private const string FIELDTYPE = Field.TYPE_DATE;

        private Field field;

        public TestDateField_A()
        {
            this.Id = PrefabField.TESTDATEFIELD_A;
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