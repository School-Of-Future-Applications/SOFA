using SOFA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOFA.Infrastructure
{
    public static class FieldOrderUtil
    {
        public static List<Field> Sort(this List<Field> @this, List<SectionFieldOrder> order)
        {
            return new List<Field>();
        }

        public static List<EnrolmentField> Sort(this List<EnrolmentField> @this, List<SectionFieldOrder> order)
        {
            return new List<EnrolmentField>();
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

        private static List<SectionFieldOrder> GetOrderForFieldIds(List<String> ids, DBContext context)
        {
            return context.SectionFieldOrders.
                        Where(sfo => ids.Contains(sfo.FieldID)).
                        OrderBy(sfo => sfo.Order).
                        ToList();
        }
    }
}