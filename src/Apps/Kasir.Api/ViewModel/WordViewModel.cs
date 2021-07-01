using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Kasir.Api.ViewModel
{
    public class WordViewModel
    {
        public int Id { get; set; }

        [Display(Name = "العنوان الافتراضي")]
        public string Title { get;  set; }

        [Display(Name = "الكلمة الافتراضي")]
        public string Name { get; set; }

        [Display(Name = "الصورة الافتراضية")]
        public string ImagePath { get; set; }

        [Display(Name = "التفاصيل الافتراضية")]
        public string Information { get; set; }

        public List<WordLanguageViewModel> WordLanguageViewModels { get; set; }

        public List<WordImageViewModel> WordImageViewModels { get; set; }
    }
}
