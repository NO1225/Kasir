using System.ComponentModel.DataAnnotations;

namespace Kasir.Application.ViewModels
{
    public class RecoverPasswordViewModel
    {

        [Required]
        [EmailAddress]
        [Display(Name = "البريد الالكتروني")]

        public string Email { get; set; }

        [Required]
        [Display(Name = "التوكين")]

        public string Token { get; set; }

        [Required]
        [Display(Name = "كلمة المرور الجديدة")]
        public string NewPassword { get; set; }

        [Required]
        [Compare(nameof(NewPassword))]
        [Display(Name = "تأكيد كلمة المرور")]
        public string ConfirmPassword { get; set; }

    }

}
