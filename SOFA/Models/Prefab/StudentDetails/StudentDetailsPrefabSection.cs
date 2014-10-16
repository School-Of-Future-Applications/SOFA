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
using SOFA.Models.Prefab;

namespace SOFA.Models.Prefab.StudentDetails
{
    public class StudentDetailsPrefabSection : PrefabSection
    {
       
        private const string NAME = "Student Information";

        private Section section;

        public StudentDetailsPrefabSection()
        {
            Id = PrefabSection.STUDENT_DETAILS;
            Name = NAME;            
        }

        public override Section GetSection()
        {            
            if (section == null)
            {
                section = new Section();
                section.Id = Id;
                section.Name = NAME;
                section.DateCreated = DateTime.Now;
                var fieldfactory = new PrefabFieldFactory();
                section.Fields.Add(fieldfactory.Get(PrefabField.FIRSTNAME));
                section.Fields.Add(fieldfactory.Get(PrefabField.LASTNAME));
                section.Fields.Add(fieldfactory.Get(PrefabField.PHONE_NUMBER));
                section.Fields.Add(fieldfactory.Get(PrefabField.MOBILE_NUMBER));
                section.Fields.Add(fieldfactory.Get(PrefabField.STUDENT_EMAIL));
                
            }

            return section;
        }
    }
}