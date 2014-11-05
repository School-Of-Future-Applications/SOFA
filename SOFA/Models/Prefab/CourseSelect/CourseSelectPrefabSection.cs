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

namespace SOFA.Models.Prefab.CourseSelect
{
    public class CourseSelectPrefabSection : PrefabSection
    {
        private const string NAME = "Enrolled Class";

        private Section section;

        public CourseSelectPrefabSection()
        {
            this.Id = PrefabSection.COURSE_SELECT;
            this.Name = NAME;
        }

        public override Section GetSection()
        {
            if (this.section == null)
            {
                this.section = new Section();
                section.Id = this.Id;
                section.Name = this.Name;
                section.DateCreated = DateTime.Now;

                PrefabFieldFactory factory = new PrefabFieldFactory();

                section.Fields.Add(factory.Get(PrefabField.CLASS_SELECT));
            }

            return section;
        }
    }
}