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
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace SOFA.Models
{
    public class FormSection 
    {
        private const int ABOVE = 1;
        private const int BELOW = -1;

        [Key, Column(Order = 1)]
        public String FormId { get; set; }

        [Key, Column(Order = 2)]
        public String SectionId { get; set; }

        public virtual Form Form { get; set; }

        public virtual Section Section { get; set; }

        public virtual Section BelowOf { get; set; }


        public static ICollection<FormSection> Sort(ICollection<FormSection> formSections)
        {
            List<FormSection> list = (List<FormSection>)formSections;
            //Find form section where below of == null. Put it at the top.
            for (int i = 0; i < list.Count; i++ )
            {
                if (list[i].BelowOf == null)
                {
                    FormSection temp = list[0];
                    list[0] = list[i];
                    list[i] = temp;
                    break;
                }
            }
            
            for (int i = 1; i < list.Count - 1; i++ )
            {
                for (int j = i; j < list.Count ; j++)
                {
                    if (list[j].BelowOf == list[i - 1].Section)
                    {
                        FormSection temp = list[j];
                        list[j] = list[i];
                        list[i] = temp;
                        break;
                    }
                }
            }
            return list;
        }
    }
}
