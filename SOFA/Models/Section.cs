using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

using SOFA.Infrastructure;

namespace SOFA.Models
{
    public class Section
    {
        public Section()
        {
            Id = UUIDUtil.NewUUID();
            Fields = new List<Field>();
            DateCreated = DateTime.Now;
        }

        public Section(String sectionName)
            : this()
        {
            Name = sectionName;
        }

        [Key]
        public string Id { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

        public string DateCreatedString()
        {
            string date = "";
            if (DateCreated != null)
                date = DateCreated.ToString(DateTimeUtil.DATE_FORMAT);
            return date;
        }

        [Required]
        [Display(Name = "Section Name")]
        public string Name { get; set; }

        public ICollection<Field> Fields;
    }
}
