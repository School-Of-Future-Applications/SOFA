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
            List<SectionFieldOrder> orders = this.CreateDummySectionFieldOrders();
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
    
        [TestMethod]
        public void Field_Sort_Test()
        {
            List<SectionFieldOrder> orders = CreateDummySectionFieldOrders();
            List<Field> fields = new List<Field>();
            fields.Add(new Field
            {
                Id = orders[2].FieldID,
            });
            fields.Add(new Field
            {
                Id = orders[1].FieldID                
            });
            fields.Add(new Field
            {
                Id = orders[0].FieldID                
            });

            //Test
            fields = fields.Sort(orders);
            
            //Assert
            Assert.IsTrue(fields[0].Id == orders[0].FieldID);
            Assert.IsTrue(fields[1].Id == orders[1].FieldID);
            Assert.IsTrue(fields[2].Id == orders[2].FieldID);
        }

        public List<SectionFieldOrder> CreateDummySectionFieldOrders()
        {
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

            return orders;
        }
    }
}
