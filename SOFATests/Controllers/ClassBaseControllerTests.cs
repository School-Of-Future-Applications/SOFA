using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SOFA.Controllers;
using System.Web.Mvc;

namespace SOFATests.Controllers
{
    [TestClass]
    public class ClassBaseControllerTests
    {
        private ClassBaseController controller;

        public ClassBaseControllerTests()
        {
            controller = new ClassBaseController();
        }

        [TestMethod]
        public void ClassBase_By_YearLevel_JSON_Test()
        {
            const int COURSEID = 1; //Assume course in DB
            JsonResult jResult = controller.ClassBaseYearLevels_JSON(COURSEID);

            //Assert
            Assert.IsNotNull(jResult, "Result is null");
            Assert.IsNotNull(jResult.Data, "Result Data is null");
        }
    }
}
