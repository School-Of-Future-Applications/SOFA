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

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void FieldList_Smaller_Than_OrderList_Throws_Exception_Test()
        {
            List<SectionFieldOrder> orders = CreateDummySectionFieldOrders();
            orders.RemoveAt(0);
            List<Field> fields = new List<Field>();
            fields.Add(new Field
            {
                Id = orders[1].FieldID,
            });
            fields.Add(new Field
            {
                Id = orders[1].FieldID
            });
            fields.Add(new Field
            {
                Id = orders[0].FieldID
            });
            fields = fields.Sort(orders);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void FieldList_Greater_Than_OrderList_Throws_Exception_Test()
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

            fields = fields.Sort(orders);
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
