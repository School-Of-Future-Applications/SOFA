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

using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Linq;

using SOFA.Infrastructure.Users;
using SOFA.Models;

namespace SOFA.Infrastructure
{
    interface INavProvider
    {
        Enum NavProviderTerm();
    }

    public struct NavInfo
    {
        public string actionName;
        public string controllerName;
        public string displayName;
        public string requiredAuth;
    }

    public struct NavSection
    {
        public string sectionName;
        public Dictionary<Enum, NavInfo> navItems;
        public string requiredAuth;
    }

    public enum DashboardNavTerms
    {
        DepartmentCourse
       ,Forms
       ,None
       ,Sections
       ,SystemConfig
       ,Timetabling
       ,UserAdmin
    }

    public static class NavUtil
    {
        public static List<NavSection> DashboardSections =
            new List<NavSection>
        {
            {new NavSection
                {
                    sectionName = null
                   ,requiredAuth = SOFA.Models.SOFARole.AUTH_TEACHER
                   ,navItems = new Dictionary<Enum,NavInfo>
                   {
                       {DashboardNavTerms.DepartmentCourse
                       ,new NavInfo {actionName = "Index"
                                    ,controllerName = "Department"
                                    ,displayName = "Department & Courses"
                                    ,requiredAuth = SOFA.Models.SOFARole.AUTH_TEACHER}}

                      ,{DashboardNavTerms.Timetabling
                       ,new NavInfo {actionName = "Index"
                                    ,controllerName = "Timetable"
                                    ,displayName = "Timetabling"
                                    ,requiredAuth = SOFA.Models.SOFARole.AUTH_TEACHER}}
                   }
                }
            }

           ,{new NavSection
                {
                    sectionName = "Forms"
                    ,requiredAuth = SOFA.Models.SOFARole.AUTH_MODERATOR
                    ,navItems = new Dictionary<Enum,NavInfo>
                    {
                        {DashboardNavTerms.Forms
                        ,new NavInfo {actionName = "Index"
                                     ,controllerName = "Form"
                                     ,displayName = "Enrollment Forms"
                                     ,requiredAuth = SOFA.Models.SOFARole.AUTH_MODERATOR}}

                        ,{DashboardNavTerms.Sections
                         ,new NavInfo {actionName = "Index"
                                     ,controllerName = "Section"
                                     ,displayName = "Enrollment Sections"
                                     ,requiredAuth = SOFA.Models.SOFARole.AUTH_MODERATOR}}
                    }
                }
            }

           ,{new NavSection
                {
                    sectionName = "SOFA Administration"
                   ,requiredAuth = SOFA.Models.SOFARole.AUTH_SOFAADMIN
                   ,navItems = new Dictionary<Enum,NavInfo>
                   {
                        {DashboardNavTerms.UserAdmin
                        ,new NavInfo {actionName = "Index"
                                     ,controllerName = "UserAdmin"
                                     ,displayName = "User Administration"
                                    ,requiredAuth = SOFA.Models.SOFARole.AUTH_SOFAADMIN}}
                   }
                }
            }

           ,{new NavSection
                {
                    sectionName = "System Administration"
                   ,requiredAuth = SOFA.Models.SOFARole.AUTH_SYSADMIN
                    ,navItems = new Dictionary<Enum,NavInfo>
                    {
                        {DashboardNavTerms.SystemConfig
                        ,new NavInfo {actionName = "Index"
                                     ,controllerName = "Settings"
                                     ,displayName = "System Settings"
                                    ,requiredAuth = SOFA.Models.SOFARole.AUTH_SYSADMIN}}
                    }
                }
            }
        };

        public static MvcHtmlString DashboardNavigation(this HtmlHelper html)
        {
            SOFAUser currentUser = html.CurrentUser();
            String navHtml = "";
            SOFAUserManager userManager = html.UserManager();

            foreach (NavSection ns in DashboardSections)
            {
                if(userManager.IsInRoles(currentUser.Id, ns.requiredAuth))
                    navHtml += DashboardSection(html, ns, currentUser, userManager).ToHtmlString();
            }
            return new MvcHtmlString(navHtml);
        }

        public static MvcHtmlString DashboardSection(this HtmlHelper html
                                                    ,NavSection section
                                                    ,SOFAUser currentUser
                                                    ,SOFAUserManager userManager)
        {
            MvcHtmlString link;
            TagBuilder liTag;
            INavProvider provider;
            TagBuilder ulTag = new TagBuilder("ul");
            NavInfo value;

            ulTag.AddCssClass("nav nav-sidebar");

            if(section.sectionName != null)
            {
                liTag = new TagBuilder("li");
                liTag.AddCssClass("heading");
                liTag.InnerHtml = section.sectionName;
                ulTag.InnerHtml += liTag;
            }

            foreach(KeyValuePair<Enum, NavInfo> k in section.navItems)
            {
               provider = html.ViewContext.Controller.ControllerContext.Controller as INavProvider;
               value = k.Value;
               if(userManager.IsInRoles(currentUser.Id, section.requiredAuth))
                {
                    liTag = new TagBuilder("li");

                    if (provider != null)
                        if (provider.NavProviderTerm().CompareTo(k.Key) == 0)
                            liTag.AddCssClass("active");

                    link = LinkExtensions.ActionLink(html, value.displayName
                                                    , value.actionName
                                                    , value.controllerName);

                    liTag.InnerHtml = link.ToHtmlString();
                    ulTag.InnerHtml += liTag;
                }
            }

            return new MvcHtmlString(ulTag.ToString());
        }
    }
}