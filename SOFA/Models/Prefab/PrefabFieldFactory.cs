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
using SOFA.Models.Prefab.StudentDetails;
using SOFA.Models.Prefab.CourseSelect;
using SOFA.Models.Prefab.StandardSection;

namespace SOFA.Models.Prefab
{
    public class PrefabFieldFactory
    {
        public Field Get(string fieldId)
        {
            switch (fieldId)
            {
                case PrefabField.FIRSTNAME:
                    return new FirstName().GetField();
                case PrefabField.LASTNAME:
                    return new LastName().GetField();
                case PrefabField.PHONE_NUMBER:
                    return new PhoneNumber().GetField();
                case PrefabField.MOBILE_NUMBER:
                    return new MobileNumber().GetField();
                case PrefabField.STUDENT_EMAIL:
                    return new Email().GetField();
                case PrefabField.CLASS_SELECT:
                    return new CourseSelectField().GetField();
                case PrefabField.TESTFIELD_A:
                    return new TestField_A().GetField();
                case PrefabField.TESTDATEFIELD_A:
                    return new TestDateField_A().GetField();
                default:
                    throw new ArgumentOutOfRangeException("Unknown prefab field");

            }

        }
    }
}