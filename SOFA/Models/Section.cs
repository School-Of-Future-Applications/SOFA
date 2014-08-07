using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

using SOFA.Infrastructure;

namespace SOFA
{
    public class Section
    {
        public Section()
        {
            Id = UUIDUtil.NewUUID();
            Fields = new List<Field>();
        }

        [Key]
        public string Id { get; set; }

        public DateTime DateCreated { get; set; }

        public string Name { get; set; }

        public ICollection<Field> Fields;
    }
}
