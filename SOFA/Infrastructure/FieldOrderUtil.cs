using SOFA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOFA.Infrastructure
{
    /// <summary>
    /// Utility class for ordering of EnrolmentFields and Fields
    /// </summary>
    public static class FieldOrderUtil
    {
        public static List<Field> Sort(this List<Field> @this, List<SectionFieldOrder> order)
        {
            if (@this.Count > order.Count)
                throw new ArgumentException("The EnrolmentField list contains more elements than the order list.");
            else if (@this.Count < order.Count)
                throw new ArgumentException("The EnrolmentField list contains less elements than the order list.");
            else
            {
                return @this.OrderBy(f => order.Single(sfo => sfo.FieldID == f.Id).
                                            Order).ToList();
                 
            }
        }

        public static List<EnrolmentField> Sort(this List<EnrolmentField> @this, List<SectionFieldOrder> order) 
        {
            if (@this.Count > order.Count)
                throw new ArgumentException("The EnrolmentField list contains more elements than the order list.");
            else if (@this.Count < order.Count)
                throw new ArgumentException("The EnrolmentField list contains less elements than the order list.");
            else
            {
                @this = @this.OrderBy(ef => order.Single(sfo => sfo.FieldID == ef.OriginalFieldId).
                                            Order).ToList();
                return @this;
            }
        }

        public static List<SectionFieldOrder> GetOrderForFields(List<Field> fields, DBContext context)
        {
            List<String> fieldIds = fields.Select(ef => ef.Id).ToList();
            return GetOrderForFieldIds(fieldIds, context);
        }

        public static List<SectionFieldOrder> GetOrderForEnrolmentFields(List<EnrolmentField> fields, DBContext context)
        {
            List<String> fieldIds = fields.Select(ef => ef.OriginalFieldId).ToList();
            return GetOrderForFieldIds(fieldIds, context);

        }

        public static List<SectionFieldOrder> GetOrderForFieldIds(List<String> ids, DBContext context)
        {
            return context.SectionFieldOrders.
                        Where(sfo => ids.Contains(sfo.FieldID)).
                        OrderBy(sfo => sfo.Order).
                        ToList();
        }
    }
}