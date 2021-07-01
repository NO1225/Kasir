using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Kasir.Api.ViewModel
{
    public class CountryViewModel
    {
        public int Id { get; set; }

        [Display(Name = "الاسم")]
        public string Name { get; set; }

        [Display(Name = "الصورة")]
        public string ImagePath { get; set; }

        public List<CountryLanguageViewModel> CountryLanguageViewModels { get; set; }

    }
}
