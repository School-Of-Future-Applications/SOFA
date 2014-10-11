using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SOFA.Models;
using SOFA.Infrastructure;
using System.Collections.Generic;

namespace SOFATests.Util
{
    [TestClass]
    public class FieldOrderUtilTests
    {
        [TestMethod]
        public void EnrolmentField_Sort_Test()
        {
            //Create SFOS
            List<SectionFieldOrder> orders = new List<SectionFieldOrder>();
            orders.Add(new SectionFieldOrder
            {
                FieldID = "First",
                Order = 1
            });
            orders.Add(new SectionFieldOrder
            {
                FieldID = "Second",
                Order = 2
            });
            orders.Add(new SectionFieldOrder
            {
                FieldID = "Third",
                Order = 3
            });
            //Create EFs
            List<EnrolmentField> efs = new List<EnrolmentField>();
            efs.Add(new EnrolmentField
                {
                    OriginalFieldId = orders[2].FieldID,
                    EnrolmentFieldId = "3"
                });
            efs.Add(new EnrolmentField
            {
                OriginalFieldId = orders[1].FieldID,
                EnrolmentFieldId = "2"
            });
            efs.Add(new EnrolmentField
            {
                OriginalFieldId = orders[0].FieldID,
                EnrolmentFieldId = "1"
            });
            //Test
            efs = efs.Sort(orders);

            //Assert
            Assert.IsTrue(efs[0].OriginalFieldId == orders[0].FieldID);
            Assert.IsTrue(efs[1].OriginalFieldId == orders[1].FieldID);
            Assert.IsTrue(efs[2].OriginalFieldId == orders[2].FieldID);
        }
    }
}
