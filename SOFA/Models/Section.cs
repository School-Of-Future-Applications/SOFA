using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace SOFA
{
    public class Section
    {
        [Key]
        public string ID { get; set; }

        public DateTime DateCreated { get; set; }

        public string Name { get; set; }

        public ICollection<Field> Fields;
    }
}
