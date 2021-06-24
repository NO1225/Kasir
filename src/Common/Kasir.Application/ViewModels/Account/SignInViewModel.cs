using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Kasir.Application.ViewModels
{
    public class SignInViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name ="البريد الالكتروني")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "كلمة المرور")]
        public string Password { get; set; }

    }


}
