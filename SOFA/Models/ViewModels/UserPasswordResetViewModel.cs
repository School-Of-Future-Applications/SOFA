using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SOFA.Models.ViewModels
{
    public class UserPasswordResetViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Compare("NewPassword")]
        public String ConfirmNewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public String NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public String OldPassword { get; set; }

        [Required]
        public String Token { get; set; }

        [Required]
        public String UserName { get; set; }
    }
}