using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SOFA.Models
{
    public class SOFAUser : IdentityUser
    {
        [DefaultValue(false)]
        public bool Active { get; set; }
    }
}