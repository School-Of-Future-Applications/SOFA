using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SOFA.Models;
using SOFA.Models.ViewModels.FormViewModels;

namespace SOFATests.FormViewModels
{
    [TestClass]
    public class FieldViewModelTests
    {
        [TestMethod]
        public void EnrolmentField_to_FieldVM_ConversionTest()
        {
            EnrolmentField field = new EnrolmentField()
            {
                EnrolmentFieldId = "id",
                FieldType = Field.TYPE_DATE,
                PromptValue = "Date",
                Value = null
            };
            FieldViewModel fvm = new FieldViewModel(field);
            var testExpr = fvm.FieldId == field.EnrolmentFieldId &&
                            fvm.FieldType == field.FieldType &&
                            fvm.PromptValue == field.PromptValue &&
                            fvm.Value == field.Value;
            Assert.IsTrue(testExpr);
        }
    }
}
