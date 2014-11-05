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

using SOFA.Models;
using System.Web.Routing;

namespace SOFA.Models.ViewModels
{
    public class EnrolmentFormViewModel
    {
        public String ApproveAction { get; set; }
        public Dictionary<String, String> ApproveArgs { get; set; }
        public String ApproveController { get; set; }
        public String DeleteAction { get; set; }
        public Dictionary<String, String> DeleteArgs { get; set; }
        public String DeleteController { get; set; }
        public String DisapproveAction { get; set; }
        public Dictionary<String, String> DisapproveArgs { get; set; }
        public String DisapproveController { get; set; }
        public EnrolmentForm EnrolmentForm { get; set; }

        public EnrolmentFormViewModel()
        {
            this.ApproveArgs = new Dictionary<String, String>();
            this.DeleteArgs = new Dictionary<String, String>();
            this.DisapproveArgs = new Dictionary<String, String>();
        }
    }
}