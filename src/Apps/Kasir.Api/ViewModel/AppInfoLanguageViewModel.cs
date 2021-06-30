using System.ComponentModel.DataAnnotations;

namespace Kasir.Api.ViewModel
{
    public class AppInfoLanguageViewModel
    {
        public int LanguageId { get; set; }

        [Display(Name = "Language")]
        public string Language { get; set; }

        [Display(Name = "Title")]
        public string Title { get; set; }


        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}
