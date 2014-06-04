using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SOFA.Models.ViewModels
{
    public class UserActivationViewModel
    {
        [Compare("Password")]
        [DataType(DataType.Password)]
        [Required]
        public String ConfirmPassword { get; set; }

        [DataType(DataType.Password)]
        [Required]
        public String Password { get; set; }

        [Required]
        public String Token { get; set; }

        [Required]
        public String UserId { get; set; }

        [Required]
        public String UserName { get; set; }
    }
}