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
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SOFA.Models
{
    public class ClassBase
    {
        public const int CLASSBASECODE_LENGTH = 30;
        public const int YEARLEVEL_LENGTH = 10;

        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(ClassBase.CLASSBASECODE_LENGTH)]
        public String ClassBaseCode { get; set; }

        [Required]
        [StringLength(ClassBase.YEARLEVEL_LENGTH)]
        public String YearLevel { get; set; }

        public bool Deleted { get; set; }
                
        public virtual Course Course { get; set; }
        
        public virtual List<TimetabledClass> TimetabledClasses{ get; set; }

        public ClassBase()
        {
            this.TimetabledClasses = new List<TimetabledClass>();
        }
    }
}