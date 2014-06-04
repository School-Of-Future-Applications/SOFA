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
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SOFA.Models.ViewModels
{
    public class UserPasswordResetViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Compare("NewPassword")]
        public String ConfirmNewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public String NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public String OldPassword { get; set; }

        [Required]
        public String Token { get; set; }

        [Required]
        public String UserName { get; set; }
    }
}