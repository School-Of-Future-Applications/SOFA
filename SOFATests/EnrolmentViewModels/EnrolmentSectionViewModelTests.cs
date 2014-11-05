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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SOFA.Models;
using SOFA.Models.ViewModels.EnrolmentViewModels;
using System.Collections.Generic;

namespace SOFATests.EnrolmentViewModels
{
    [TestClass]
    public class EnrolmentSectionViewModelTests
    {
        private EnrolmentSection eSection;
        private EnrolmentSectionViewModel esvm;

        [TestInitialize]
        public void InitialiseTest()
        {
            eSection = new EnrolmentSection();
            eSection.Id = "abc-123";
            eSection.SectionName = "Test Section Name";
            eSection.DateCreated = DateTime.Now;

            esvm = new EnrolmentSectionViewModel();
            esvm.SectionId = "abc-123";
            esvm.SectionName = "Test Section Name";
            esvm.DateCreated = DateTime.Now;
        }

        [TestMethod]
        public void EnrolmentSectionVM_Contructor_Equal_Values_Test()
        {
            EnrolmentSectionViewModel esvm = new EnrolmentSectionViewModel(eSection);

            Assert.AreEqual(eSection.Id, esvm.SectionId, "Ids aren't equal");
            Assert.AreEqual(eSection.SectionName, esvm.SectionName, "Section name isn't equal");
            Assert.AreEqual(eSection.DateCreated, esvm.DateCreated, "Date created isn't equal");
        }

        [TestMethod]
        public void EnrolmentSectionVM_Conversion_Values_Equal_Test()
        {
            EnrolmentSection es = this.esvm.toEnrolmentSection();

            Assert.AreEqual(this.esvm.SectionId, es.Id);
            Assert.AreEqual(this.esvm.SectionName, es.SectionName);
            Assert.AreEqual(this.esvm.DateCreated, es.DateCreated);

        }

        [TestMethod]
        public void EnrolmentSectionVM_Constructor_FieldVM_Conversion_NotNULL_Test()
        {
            EnrolmentField eField = new EnrolmentField()
            {
                FieldType = Field.TYPE_TEXT_SINGLE,
                PromptValue = "Test prompt",
                Value = "Test Value"
            };
            eField.EnrollmentFieldOptions.Add(new EnrolmentFieldOption()
                {
                     OptionType = FieldOption.OPT_MANDATORY,
                     OptionValue = "TRUE"
                });
            this.eSection.EnrolmentFields.Add(eField);
            EnrolmentSectionViewModel esvm = new EnrolmentSectionViewModel(this.eSection);

            var vmFields = (List<EnrolmentFieldViewModel>) esvm.EnrolmentFields;
            Assert.IsNotNull(vmFields[0]);
        }
    }
}
