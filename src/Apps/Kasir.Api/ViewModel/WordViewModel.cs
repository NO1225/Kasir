using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Kasir.Api.ViewModel
{
    public class WordViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Default WS Name")]
        public string Title { get;  set; }

        [Display(Name = "Default Dsc Code")]
        public string Name { get; set; }

        [Display(Name = "Default Image")]
        public string ImagePath { get; set; }

        [Display(Name = "Default Dsc Details")]
        public string Information { get; set; }

        public List<WordLanguageViewModel> WordLanguageViewModels { get; set; }

        public List<WordImageViewModel> WordImageViewModels { get; set; }
    }
}
