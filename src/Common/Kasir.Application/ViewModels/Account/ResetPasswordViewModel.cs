using System.ComponentModel.DataAnnotations;

namespace Kasir.Application.ViewModels
{
    public class ResetPasswordViewModel
    {

        [Required]
        public string Id { get; set; }

        [Required]
        [Display(Name = "كلمة المرور الجديدة")]
        public string NewPassword { get; set; }

        [Required]
        [Compare(nameof(NewPassword))]
        [Display(Name = "تأكيد كلمة المرور")]
        public string ConfirmPassword { get; set; }

    }

}
