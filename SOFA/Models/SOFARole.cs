using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;

namespace SOFA.Models
{
    public class SOFARole : IdentityRole
    {
        public const String MODERATOR_ROLE = "moderator";
        public const String NONE_ROLE = "none";
        public const String SOFAADMIN_ROLE = "sofaadmin";
        public const String SYSADMIN_ROLE = "sysadmin";
        public const String TEACHER_ROLE = "teacher";

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