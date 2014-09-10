using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SOFA.Models;
using SOFA.Models.ViewModels.FormViewModels;

namespace SOFATests.FormViewModels
{
    [TestClass]
    public class SectionViewModelTests
    {
        [TestMethod]
        public void SectionConversionTest()
        {
            Section section = new Section()
            {
                Id = "id",
                DateCreated = DateTime.Now,
                Name = "Section Name"
            };
            SectionViewModel svm = new SectionViewModel(section);
            var testExpr = svm.Id == section.Id &&
                            svm.Name == section.Name;
            Assert.IsTrue(testExpr);
        }
    }
}
