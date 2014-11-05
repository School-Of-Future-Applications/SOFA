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

using SOFA.Models;
using SOFA.Models.Prefab;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOFA.Infrastructure
{
    /// <summary>
    /// Utility class for adding and removing prequisite
    /// sections from forms.
    /// </summary>
    public class PrereqUtil
    {

        public List<EnrolmentFormSection> CollectAndAppendPrerequisiteSections(List<EnrolmentFormSection> formSections, ClassBase cb)
        {
            
            List<EnrolmentSection> prereqs = new List<EnrolmentSection>();
            foreach (Section pr in cb.PreRequisites)
            {
                if (formSections.SingleOrDefault(ef => ef.EnrolmentSection.OriginalSectionId == pr.Id)
                        == null)
                    prereqs.Add(new EnrolmentSection(pr));
            }
            // Get the class select section and its index 
            var classSelectSection = formSections.
                                        Single(ef => ef.EnrolmentSection.OriginalSectionId == PrefabSection.COURSE_SELECT).
                                        EnrolmentSection;
            EnrolmentFormSection nextSectionAfterInsert = formSections.SingleOrDefault(fs => fs.BelowOf == classSelectSection);
            //Insert each pre-reqsection into form under class select section
            List<EnrolmentFormSection> efSections = new List<EnrolmentFormSection>();
            for (int i = 0; i < prereqs.Count; i++)
            {
                EnrolmentFormSection efs = new EnrolmentFormSection()
                {
                    EnrolmentSection = prereqs.ElementAt(i)
                };
                if (i == 0)
                    efs.BelowOf = classSelectSection;
                else
                    efs.BelowOf = prereqs.ElementAt(i - 1);
                efSections.Add(efs);
            }
            formSections = EnrolmentFormSection.Sort(formSections).ToList();
            int insertIndex = formSections.IndexOf(formSections. 
                                    Single(efs => efs.EnrolmentSection == classSelectSection)) + 1;
            formSections.InsertRange(insertIndex, efSections);
            //Get the previous next section and link it to the last prereq
            if (nextSectionAfterInsert != null)
                nextSectionAfterInsert.BelowOf = prereqs.ElementAt(prereqs.Count - 1);

            return formSections;
            
        }

        public List<EnrolmentFormSection> RemoveAllPrerequisiteSections(List<EnrolmentFormSection> formSections, DBContext context)
        {
            formSections = EnrolmentFormSection.Sort(formSections).ToList();
            var listsOfPrereqs = context.ClassBases.Select(cb => cb.PreRequisites).ToList();
            List<String> prereqSectionids = new List<string>();
            foreach (List<Section> sections in listsOfPrereqs)
            {
                foreach (Section sec in sections)
                {
                    prereqSectionids.Add(sec.Id);
                }
            }
            formSections.
                RemoveAll(efs => prereqSectionids.Contains(efs.EnrolmentSection.OriginalSectionId));
                foreach (var fs in formSections)
                {
                    if (fs == formSections.First())
                        fs.BelowOf = null;
                    else
                        fs.BelowOf = formSections[formSections.IndexOf(fs) - 1].EnrolmentSection;
                }
                formSections = EnrolmentFormSection.Sort(formSections).ToList();
            return formSections;
        }
    
        
    
    }


}