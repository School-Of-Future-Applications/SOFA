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
        public void DateFieldValidTest()
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
        public void DateFieldInValidTest()
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
        public void MandFieldValidTest()
        {
            // Test NULL value
            EnrolmentField field = new EnrolmentField()
            {
                FieldType = Field.TYPE_TEXT_SINGLE,
                Value = " Valid value"
            };
            field.EnrollmentFieldOptions.Add(new EnrolmentFieldOption
                {
                    OptionType = FieldOption.OPT_MANDATORY
                }
            );
            List<ValidationResult> results = EnrolmentValidator.ValidateField(field).ToList();
            var resultExpr = results.Count == 0 || results[0] == ValidationResult.Success;
            Assert.IsTrue(resultExpr, "Field passed validation but shouldn't have.");
        }

        [TestMethod]
        public void MandFieldInvalidTest()
        {
            // Test NULL value
            EnrolmentField field = new EnrolmentField()
            {
                FieldType = Field.TYPE_TEXT_SINGLE,                
            };
            field.EnrollmentFieldOptions.Add(new EnrolmentFieldOption
                {
                    OptionType = FieldOption.OPT_MANDATORY
                }
            );
            var results = EnrolmentValidator.ValidateField(field).ToList();
            var resultExpr = results.Count == 0 || results[0] == ValidationResult.Success;
            Assert.IsFalse(resultExpr, "Field passed validation but shouldn't have.");

            //Test whitespace value
            field.Value = "    ";
            results = EnrolmentValidator.ValidateField(field).ToList();
            resultExpr = results.Count == 0 || results[0] == ValidationResult.Success;
            Assert.IsFalse(resultExpr, "Field passed validation but shouldn't have.");
        }
    
    }
}
