using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SOFA.Models;
using SOFA.Models.Prefab;
using SOFA.Controllers;

namespace SOFATests.Controllers
{
    [TestClass]
    public class SectionControllerTests
    {
        [TestMethod]
        public void Section_Delete_Test()
        {
            var id = PrefabSection.STANDARD_SECTION;
            SectionController controller = new SectionController();
            Assert.IsNotNull(controller.DeletePost(id));
        }
    }
}
