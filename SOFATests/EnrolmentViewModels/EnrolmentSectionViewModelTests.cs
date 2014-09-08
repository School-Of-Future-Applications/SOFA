using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SOFA.Models;
using SOFA.Models.ViewModels.EnrolmentViewModels;

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
        public void EnrolmentSectionVM_Contructor()
        {
            EnrolmentSectionViewModel esvm = new EnrolmentSectionViewModel(eSection);

            Assert.AreEqual(eSection.Id, esvm.SectionId, "Ids aren't equal");
            Assert.AreEqual(eSection.SectionName, esvm.SectionName, "Section name isn't equal");
            Assert.AreEqual(eSection.DateCreated, esvm.DateCreated, "Date created isn't equal");
        }

        [TestMethod]
        public void EnrolmentSection_Conversion()
        {
            EnrolmentSection es = this.esvm.toEnrolmentSection();

            Assert.AreEqual(this.esvm.SectionId, es.Id);
            Assert.AreEqual(this.esvm.SectionName, es.SectionName);
            Assert.AreEqual(this.esvm.DateCreated, es.DateCreated);

        }
    }
}
