using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Kasir.Api.ViewModel
{
    public class WordViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Dsc Code")]
        public string Name { get; set; }

        [Display(Name = "Image")]
        public string ImagePath { get; set; }

        public List<WordLanguageViewModel> WordLanguageViewModels { get; set; }

        public List<WordCountryViewModel> WordCountryViewModels { get; set; }
    }
}
