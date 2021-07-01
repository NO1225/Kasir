using System.ComponentModel.DataAnnotations;

namespace Kasir.Api.ViewModel
{
    public class WordLanguageViewModel
    {
        public int LanguageId { get; set; }

        [Display(Name = "اللغة")]
        public string Language { get; set; }

        [Display(Name = "العنوان")]
        public string WordTitle { get; set; }

        [Display(Name = "الاسم")]
        public string WordName { get; set; }

        [Display(Name = "التفاصيل")]
        public string WordInformation { get; set; }
    } 
}
