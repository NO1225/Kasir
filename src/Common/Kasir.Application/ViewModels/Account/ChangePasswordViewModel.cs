using System.ComponentModel.DataAnnotations;

namespace Kasir.Application.ViewModels
{
    public class ChangePasswordViewModel
    {

        [Required]
        [Display(Name = "كلمة المرور القديمة")]
        public string OldPassword { get; set; }

        [Required]
        [Display(Name = "كلمة المرور الجديدة")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$")]
        public string NewPassword { get; set; }

        [Required]
        [Compare(nameof(NewPassword))]
        [Display(Name = "تأكيد كلمة المرور")]
        public string ConfirmPassword { get; set; }

    }

}
