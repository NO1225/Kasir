using System.ComponentModel.DataAnnotations;

namespace Kasir.Api.ViewModel
{
    public class CountryLanguageViewModel
    {
        public int LanguageId { get; set; }

        [Display(Name = "Language")]
        public string Language { get; set; }

        [Display(Name = "Name")]
        public string CountryName { get; set; }

    }
}
