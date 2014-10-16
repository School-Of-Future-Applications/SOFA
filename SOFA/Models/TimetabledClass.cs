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
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SOFA.Models
{
    public class TimetabledClass
    {
        public const double THRESHOLD_PERCANTAGE = 60;

        [Key]
        public int Id { get; set; }

        public int? ClassBaseID { get; set; }
        [ForeignKey("ClassBaseID")]
        public virtual ClassBase ClassBase { get; set; }
        
        

        public virtual Line Line { get; set; }

        public Int32 Capacity { get; set; }

        public virtual Person Teacher { get; set; }

        [StringLength(100)]
        [Display(Name = "Display Name")]
        public String DisplayName { get; set; }

        public virtual ICollection<EnrolmentForm> EnrolmentForms { get; set; }

        public double fillPercantage()
        {
            int enrolledCount = validEnrolmentCount();
            return ((double)enrolledCount / (double)Capacity * 100);
        }

        public bool isAlmostFull()
        {
            double fill = fillPercantage();
            return fillPercantage() >= THRESHOLD_PERCANTAGE;
        }

        public bool isFull()
        {
            int enrolledCount = EnrolmentForms.Where(x => x.Status == EnrolmentForm.EnrolmentStatus.Approved ||
                                                     x.Status == EnrolmentForm.EnrolmentStatus.Completed).Count();
            return enrolledCount >= Capacity;
        }

        public int validEnrolmentCount()
        {
            return EnrolmentForms.Where(x => x.Status == EnrolmentForm.EnrolmentStatus.Approved ||
                                                     x.Status == EnrolmentForm.EnrolmentStatus.Completed).Count();
        }
    }
}