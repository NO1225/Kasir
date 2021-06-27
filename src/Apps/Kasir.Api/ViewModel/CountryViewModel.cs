using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Kasir.Api.ViewModel
{
    public class CountryViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Image")]
        public string ImagePath { get; set; }

        public List<CountryLanguageViewModel> CountryLanguageViewModels { get; set; }

    }
}
