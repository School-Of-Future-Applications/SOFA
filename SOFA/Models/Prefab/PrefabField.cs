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
using System.Text;
using System.Threading.Tasks;

namespace SOFA.Models.Prefab
{
    public abstract class PrefabField
    {
        /* Student fields */
        public const string FIRSTNAME = "14317bf5-199e-4c00-b703-a74fe76cc8d3";
        public const string LASTNAME = "d5b3bd93-75cf-4c23-ac60-b4a0b09a4282";
        public const string STUDENT_EMAIL = "8b3b03a7-adf9-4735-8e39-1343048884d1";
        public const string PHONE_NUMBER = "28a7c0e8-ea47-49da-98f7-546d66a641db";
        public const string MOBILE_NUMBER = "8970bec8-f49f-4d32-b231-6c7c8fa59dfb";

        /* Course Select Fields */
        public const string CLASS_SELECT = "7677bdf6-24ad-47b9-b6a5-129cf1a93ddc";

        /* StandardSection fields */
        public const string TESTFIELD_A = "TESTFIELD_A_ID";
        public const string TESTDATEFIELD_A = "TESTDATEFIELD_ID";

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
