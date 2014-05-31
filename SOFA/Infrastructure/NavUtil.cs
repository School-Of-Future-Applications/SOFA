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
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace SOFA.Infrastructure
{
    interface INavProvider
    {
        DashboardNavTerms NavProviderTerm();
    }

    public struct NavInfo
    {
        public string actionName;
        public string controllerName;
        public string displayName;
    }

    public enum DashboardNavTerms
    {
        DepartmentCourse
       ,None
       ,Timetabling
       ,UserAdmin
    }

    public static class NavUtil
    {
        public static Dictionary<DashboardNavTerms, NavInfo> DashboardNavItems =
            new Dictionary<DashboardNavTerms, NavInfo>
        {
             {DashboardNavTerms.DepartmentCourse
             ,new NavInfo {actionName = "Index", controllerName = "Department"
                          ,displayName = "Department & Courses"}}

            ,{DashboardNavTerms.Timetabling
             ,new NavInfo {actionName = "Index", controllerName = "Timetable"
                          ,displayName = "Timetabling"}}      
            
            ,{DashboardNavTerms.UserAdmin
             ,new NavInfo {actionName="Index", controllerName = "UserAdmin"
                          ,displayName = "User Administration"}}
        };

        public static MvcHtmlString DashboardNavigation(this HtmlHelper html)
        {
            MvcHtmlString link;
            TagBuilder liTag;
            INavProvider provider;
            ReflectedControllerDescriptor rcd = null;
            TagBuilder ulTag = new TagBuilder("ul");
            NavInfo value;

            ulTag.AddCssClass("nav nav-sidebar");

            foreach(KeyValuePair<DashboardNavTerms, NavInfo> k in DashboardNavItems)
            {
                provider = html.ViewContext.Controller.ControllerContext.Controller as INavProvider;
                value = k.Value;
                liTag = new TagBuilder("li");

                if (provider != null)
                    if(provider.NavProviderTerm() == k.Key)
                        liTag.AddCssClass("active");
                   
               
   
                link = LinkExtensions.ActionLink(html, value.displayName
                                                ,value.actionName
                                                ,value.controllerName);
                liTag.InnerHtml = link.ToHtmlString();
                ulTag.InnerHtml += liTag;
            }

            return new MvcHtmlString(ulTag.ToString());
        }
    }
}