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
using System.Text;
using System.Web.Mvc;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Specialized;
using SOFA.Models.ViewModels.EnrolmentViewModels;
using SOFA.Models.Binders;
using SOFA.Models.Prefab;

namespace SOFATests.ModelBinders
{
    /// <summary>
    /// Summary description for EnrolmentSectionVMBinderTests
    /// </summary>
    [TestClass]
    public class EnrolmentSectionVMBinderTests
    {
        private EnrolmentSectionViewModel esvm;
        private StudentEnrolmentSectionViewModel sesv;
        private NameValueCollection mockFormCollection;
        

        public EnrolmentSectionVMBinderTests()
        {
            this.esvm = new EnrolmentSectionViewModel();
            var properties = esvm.GetType().GetProperties();

            //Get properties but make sure they're strings
            String firstPropertyName, secondPropertyName;
            firstPropertyName = secondPropertyName = String.Empty;
            for (int i = 0; i < properties.Length; i++)
            {
                if (properties[i].PropertyType.Equals(typeof(String)))
                {
                    if (firstPropertyName.Equals(String.Empty))
                    {
                        firstPropertyName = properties[i].Name;
                    }
                    else
                    {
                        secondPropertyName = properties[i].Name;
                        break;
                    }

                }
            }

            this.mockFormCollection = new NameValueCollection()
            {
                { firstPropertyName , "TestFirstProperty" },
                { secondPropertyName, "TestSecondProperty"}

            };
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        /*
         * Tests whether given a base model EnrolmentSectionVM,
         * a model of the base type is returned.
         */
        [TestMethod]
        public void BaseModelReturnsAsBaseModel()
        {            

            var valueProvider = new NameValueCollectionValueProvider(mockFormCollection, null);
            var metadata = ModelMetadataProviders.Current.GetMetadataForType(null, typeof(EnrolmentSectionViewModel));
            var bindingContext = new ModelBindingContext()
            {
                ModelName = "",
                ValueProvider = valueProvider,
                ModelMetadata = metadata
            };
            var controllerContext = new ControllerContext();
            var binder = new EnrolmentSectionVMBinder();

            var bindedModel = binder.BindModel(controllerContext, bindingContext);

            Assert.IsInstanceOfType(bindedModel, typeof(EnrolmentSectionViewModel));
            Assert.IsNotInstanceOfType(bindedModel, typeof(StudentEnrolmentSectionViewModel));

        }

        [TestMethod]
        public void StudentModelReturnsStudentModel()
        {

            this.mockFormCollection.Add("OriginalSectionId", PrefabSection.STUDENT_DETAILS);
            var valueProvider = new NameValueCollectionValueProvider(mockFormCollection, null);
            var metadata = ModelMetadataProviders.Current.GetMetadataForType(null, typeof(EnrolmentSectionViewModel));
            var bindingContext = new ModelBindingContext()
            {
                ModelName = "",
                ValueProvider = valueProvider,
                ModelMetadata = metadata
            };
            var controllerContext = new ControllerContext();
            var binder = new EnrolmentSectionVMBinder();

            var bindedModel = binder.BindModel(controllerContext, bindingContext);

            Assert.IsInstanceOfType(bindedModel, typeof(EnrolmentSectionViewModel));
            Assert.IsNotInstanceOfType(bindedModel, typeof(StudentEnrolmentSectionViewModel));

        }

    }
}
