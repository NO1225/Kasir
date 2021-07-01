using System.ComponentModel.DataAnnotations;

namespace Kasir.Api.ViewModel
{
    public class AppInfoLanguageViewModel
    {
        public int LanguageId { get; set; }

        [Display(Name = "اللغة")]
        public string Language { get; set; }

        [Display(Name = "العنوان")]
        public string Title { get; set; }


        [Display(Name = "التفاصيل")]
        public string Description { get; set; }
    }
}
