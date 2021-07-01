using System.ComponentModel.DataAnnotations;

namespace Kasir.Api.ViewModel
{
    public class CountryLanguageViewModel
    {
        public int LanguageId { get; set; }

        [Display(Name = "اللغة")]
        public string Language { get; set; }

        [Display(Name = "الاسم")]
        public string CountryName { get; set; }

    }
}
