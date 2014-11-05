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
using SOFA.Models;

namespace SOFA.Models.Prefab
{
    public abstract class PrefabSection
    {
        public const string STUDENT_DETAILS = "a9bd91b0-29b5-11e4-8c21-0800200c9a66";
        public const string COURSE_SELECT = "a9bd91b1-29b5-11e4-8c21-0800200c9a66";
        public const string STANDARD_SECTION = "Standard-Section-Id";

        public static List<String> GetAllPrefabSectionIds()
        {
            return new List<String>()
            {
                STUDENT_DETAILS,
                COURSE_SELECT
            };
        }

        protected string Id;
        protected string Name;

        public abstract Section GetSection();

    }
}
