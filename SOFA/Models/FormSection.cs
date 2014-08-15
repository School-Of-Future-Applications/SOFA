using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace SOFA.Models
{
    public class FormSection 
    {
        private const int ABOVE = 1;
        private const int BELOW = -1;

        [Key, Column(Order = 1)]
        public String FormId { get; set; }

        [Key, Column(Order = 2)]
        public String SectionId { get; set; }

        public virtual Form Form { get; set; }

        public virtual Section Section { get; set; }

        public virtual Section BelowOf { get; set; }


        public static void Sort(IEnumerable<FormSection> formSections)
        {
            List<FormSection> list = (List<FormSection>)formSections;
            //Find form section where below of == null. Put it at the top.
            for (int i = 0; i < list.Count; i++ )
            {
                if (list[i].BelowOf == null)
                {
                    FormSection temp = list[0];
                    list[0] = list[i];
                    list[i] = temp;
                    break;
                }
            }
            
            for (int i = 1; i < list.Count - 1; i++ )
            {
                for (int j = i; j < list.Count ; j++)
                {
                    if (list[j].BelowOf == list[i - 1].Section)
                    {
                        FormSection temp = list[j];
                        list[j] = list[i];
                        list[i] = temp;
                        break;
                    }
                }
            }
        }
    }
}
