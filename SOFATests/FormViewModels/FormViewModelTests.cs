using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SOFA.Models;
using SOFA.Models.ViewModels.FormViewModels;

namespace SOFATests.FormViewModels
{
    [TestClass]
    public class FormViewModelTests
    {
        [TestMethod]
        public void FormConversionTest()
        {
            Form form = new Form()
            {
                Id = "id",
                FormName = "Test Name",
                DateCreated = DateTime.Now,
                LastModified = DateTime.Now
            };
            FormViewModel fvm = new FormViewModel(form);
            var testExpr = fvm.Id == form.Id &&
                            fvm.FormName == form.FormName;
            Assert.IsTrue(testExpr);
        }
    }
}
