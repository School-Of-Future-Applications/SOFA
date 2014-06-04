using System;
using System.ComponentModel.DataAnnotations;

namespace SOFA.Models.ViewModels
{
    public class UserLoginViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        public String Password { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public String UserName { get; set; }
    }
}