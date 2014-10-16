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

using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;

namespace SOFA.Models
{
    public class SOFARole : IdentityRole
    {
        public const String MODERATOR_ROLE = "Moderator";
        public const String NONE_ROLE = "None";
        public const String SOFAADMIN_ROLE = "SOFA Administrator";
        public const String SYSADMIN_ROLE = "System Administrator";
        public const String TEACHER_ROLE = "Teacher";

        public const String AUTH_TEACHER = TEACHER_ROLE + "," +
                                           MODERATOR_ROLE + "," +
                                           SOFAADMIN_ROLE + "," +
                                           SYSADMIN_ROLE + ",";


        public const String AUTH_MODERATOR = MODERATOR_ROLE + "," +
                                             SOFAADMIN_ROLE + "," +
                                             SYSADMIN_ROLE;

        public const String AUTH_SOFAADMIN = SOFAADMIN_ROLE + "," +
                                             SYSADMIN_ROLE;

        public const String AUTH_SYSADMIN = SYSADMIN_ROLE;

        public static List<String> SOFA_ROLES = new List<String>()
        {
             MODERATOR_ROLE
            ,NONE_ROLE
            ,SOFAADMIN_ROLE
            ,SYSADMIN_ROLE
            ,TEACHER_ROLE
        };

        public SOFARole()
            : base()
        { }

        public SOFARole(String name)
            : base(name)
        { }

        public static String HighestListRole(IList<String> roles)
        {
            if (roles.Contains(SYSADMIN_ROLE))
                return SYSADMIN_ROLE;
            else if (roles.Contains(SOFAADMIN_ROLE))
                return SOFAADMIN_ROLE;
            else if (roles.Contains(MODERATOR_ROLE))
                return MODERATOR_ROLE;
            else if (roles.Contains(TEACHER_ROLE))
                return TEACHER_ROLE;
            return NONE_ROLE;
        }
    }
}