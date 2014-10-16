/*
 *  School Of Future Applications
 *
 *  Copyright (C) 2014  Terminal Coding
 *
 * This program is free software; you can redistribute it and/or
 * modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation; either version 2
 * of the License, or (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOFA.Models.Prefab.StudentDetails
{
    public class FirstName : PrefabField
    {
        private const string PROMPT = "First Name";
        private const string FIELDTYPE = Field.TYPE_TEXT_SINGLE;

        private Field field;

        public FirstName()
        {
            this.Id = PrefabField.FIRSTNAME;
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
        
        public LastName()
        {
            this.Id = PrefabField.LASTNAME;
            this.Prompt = PROMPT;
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

    public class Email : PrefabField
    {
        private const string PROMPT = "Email";
        private const string FIELDTYPE = Field.TYPE_TEXT_SINGLE;

        public Email()
        {
            this.Id = PrefabField.STUDENT_EMAIL;
            this.Prompt = PROMPT;
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

    public class PhoneNumber : PrefabField
    {
        private const string PROMPT = "Phone Number";
        private const string FIELDTYPE = Field.TYPE_TEXT_SINGLE;

        private Field field;

        public PhoneNumber()
        {
            this.Id = PrefabField.PHONE_NUMBER;
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

    public class MobileNumber : PrefabField
    {
        private const string PROMPT = "Mobile Number";
        private const string FIELDTYPE = Field.TYPE_TEXT_SINGLE;

        private Field field;

        public MobileNumber()
        {
            this.Id = PrefabField.MOBILE_NUMBER;
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