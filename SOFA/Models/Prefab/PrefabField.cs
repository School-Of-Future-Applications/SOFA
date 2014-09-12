﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOFA.Models.Prefab
{
    public abstract class PrefabField
    {
        public const string FIRSTNAME = "14317bf5-199e-4c00-b703-a74fe76cc8d3";
        public const string LASTNAME = "d5b3bd93-75cf-4c23-ac60-b4a0b09a4282";
        public const string STUDENT_EMAIL = "8b3b03a7-adf9-4735-8e39-1343048884d1";
        public const string PHONE_NUMBER = "28a7c0e8-ea47-49da-98f7-546d66a641db";
        public const string MOBILE_NUMBER = "8970bec8-f49f-4d32-b231-6c7c8fa59dfb";



        protected string Id;
        protected string Prompt;

        public virtual string GetId()
        {
            return this.Id;
        }
        
        public virtual string GetPromptValue()
        {
            return this.Prompt;
        }
       
        public abstract Field GetField();

    }
}
