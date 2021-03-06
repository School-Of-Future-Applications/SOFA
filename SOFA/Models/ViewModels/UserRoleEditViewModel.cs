﻿/*
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
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

using SOFA.Models.ViewModels;

namespace SOFA.Models.ViewModels
{
    public class UserRoleEditViewModel
    {
        public IEnumerable<SOFARole> AvailableRoles { get; set; }

        [Display(Name = "Current Role")]
        public string CurrentRole { get; set; }

        public string CurrentRoleId { get; set; }

        [Required]
        public string SelectedRoleId { get; set; }

        [Required]
        public string UserId { get; set; }
    }
}