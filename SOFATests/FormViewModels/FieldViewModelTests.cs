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
        public void FieldConversionTest()
        {
            EnrolmentField field = new EnrolmentField()
            {
                EnrolmentFIeldId = "id",
                FieldType = Field.TYPE_DATE,
                PromptValue = "Date",
                Value = null
            };
            FieldViewModel fvm = new FieldViewModel(field);
            var testExpr = fvm.FieldId == field.EnrolmentFIeldId &&
                            fvm.FieldType == field.FieldType &&
                            fvm.PromptValue == field.PromptValue &&
                            fvm.Value == field.Value;
            Assert.IsTrue(testExpr);
        }
    }
}
