/*
 *  School Of Future Applications
 *
 *  Copyright (C) 2014  Terminal Coding
 *
 * This program is free software; you can redistribute it and/or
 * modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation; either version 2
 * of the License, or (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
 */

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
