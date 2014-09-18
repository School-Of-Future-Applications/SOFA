using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SOFA.Controllers;
using System.Web.Mvc;

namespace SOFATests.Controllers
{
    [TestClass]
    public class CourseControllerTests
    {
        private CourseController controller;

        public CourseControllerTests()
        {
            controller = new CourseController();
        }

        [TestMethod]
        public void JSON_Course_List_Test()
        {
            const int DEPARTMENTID = 1; //Assume there's a dept in the DB
            JsonResult jResult = controller.CourseIndex_Json(DEPARTMENTID);

            Assert.IsNotNull(jResult, "Returned value is null");
            Assert.IsNotNull(jResult.Data);
        }


    }
}
