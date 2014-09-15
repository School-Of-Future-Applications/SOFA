using System;
using System.Text;
using System.Web.Mvc;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Specialized;
using SOFA.Models.ViewModels.EnrolmentViewModels;
using SOFA.Models.Binders;

namespace SOFATests.ModelBinders
{
    /// <summary>
    /// Summary description for EnrolmentSectionVMBinderTests
    /// </summary>
    [TestClass]
    public class EnrolmentSectionVMBinderTests
    {
        public EnrolmentSectionVMBinderTests()
        {
            //
            // TODO: Add constructor logic here
            //
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
            var esvm = new EnrolmentSectionViewModel();
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

            var formCollection = new NameValueCollection()
            {
                { firstPropertyName , "TestFirstProperty" },
                { secondPropertyName, "TestSecondProperty"}

            };

            var valueProvider = new NameValueCollectionValueProvider(formCollection, null);
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
