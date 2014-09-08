using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SOFA.Models;
using SOFA.Models.ViewModels.EnrolmentViewModels;

namespace SOFATests.EnrolmentViewModels
{
    [TestClass]
    public class EnrolmentSectionViewModelTests
    {
        [TestMethod]
        public void EnrolmentSection_Contructor()
        {
            EnrolmentSection es = new EnrolmentSection();
            es.Id = "abc-123";
            es.SectionName = "Test Section Name";
            es.DateCreated = DateTime.Now;

            EnrolmentSectionViewModel esvm = new EnrolmentSectionViewModel(es);

            Assert.AreEqual(es.Id, esvm.SectionId, "Ids aren't equal");
            Assert.AreEqual(es.SectionName, esvm.SectionName, "Section name isn't equal");
            Assert.AreEqual(es.DateCreated, esvm.DateCreated, "Date created isn't equal");
        }
    }
}
