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
using SOFA.Models.Validation;
using SOFA.Models;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SOFATests
{
    [TestClass]
    public class EnrolmentValidationUnitTest
    {
        [TestMethod]
        public void Valid_Date_Field_Test()
        {
            EnrolmentField field = new EnrolmentField()
            {
                FieldType = Field.TYPE_DATE,
                Value = "01/01/2013"
            };
            List<ValidationResult> results = EnrolmentValidator.ValidateField(field).ToList();
            var resultExpr = results.Count == 0 || results[0] == ValidationResult.Success;
            Assert.IsTrue(resultExpr, "Field did not pass validation but should have.");
        }

        [TestMethod]
        public void Invalid_Culture_Date_Field_Test()
        {
            EnrolmentField field = new EnrolmentField()
            {
                FieldType = Field.TYPE_DATE,
                Value = "01/13/2013"
            };
            List<ValidationResult> results = EnrolmentValidator.ValidateField(field).ToList();
            var resultExpr = results.Count == 0 || results[0] == ValidationResult.Success;
            Assert.IsFalse(resultExpr, "Field passed validation but shouldn't have.");
        }
    
        [TestMethod]
        public void Valid_Mandatory_Field_Test()
        {
            // Test NULL value
            EnrolmentField field = new EnrolmentField()
            {
                FieldType = Field.TYPE_TEXT_SINGLE,
                Value = " Valid value"
            };
            field.EnrollmentFieldOptions.Add(new EnrolmentFieldOption
                {
                    OptionType = FieldOption.OPT_MANDATORY,
                    OptionValue = FieldOption.VAL_TRUE
                }
            );
            List<ValidationResult> results = EnrolmentValidator.ValidateField(field).ToList();
            var resultExpr = results.Count == 0 || results[0] == ValidationResult.Success;
            Assert.IsTrue(resultExpr, "Field did not pass validation but should have.");
        }

        [TestMethod]
        public void Invalid_Mandatory_Field_Test()
        {
            // Test NULL value
            EnrolmentField field = new EnrolmentField()
            {
                FieldType = Field.TYPE_TEXT_SINGLE,                
            };
            field.EnrollmentFieldOptions.Add(new EnrolmentFieldOption
                {
                    OptionType = FieldOption.OPT_MANDATORY,
                    OptionValue = FieldOption.VAL_TRUE
                }
            );
            var results = EnrolmentValidator.ValidateField(field).ToList();
            Assert.IsFalse(results[0] == ValidationResult.Success, "Field passed validation but shouldn't have.");

            //Test whitespace value
            field.Value = "    ";
            results = EnrolmentValidator.ValidateField(field).ToList();
            Assert.IsFalse(results[0] == ValidationResult.Success, "Field passed validation but shouldn't have.");
        }

        [TestMethod]
        public void Valid_Numeric_Field_Test()
        {
            EnrolmentField field = new EnrolmentField()
            {
                FieldType = Field.TYPE_TEXT_SINGLE,
                Value = "1234.56"
            };

            //Test
            Assert.IsTrue(EnrolmentValidator.IsValidNumeric(field));
        }

        [TestMethod]
        public void Null_Valid_Numeric_Field_Test()
        {
            EnrolmentField field = new EnrolmentField()
            {
                FieldType = Field.TYPE_TEXT_SINGLE,
                Value = null
            };

            //Test
            Assert.IsTrue(EnrolmentValidator.IsValidNumeric(field));
        }
    
    }
}
