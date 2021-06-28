using System.ComponentModel.DataAnnotations;

namespace Kasir.Api.ViewModel
{
    public class WordLanguageViewModel
    {
        public int LanguageId { get; set; }

        [Display(Name = "Language")]
        public string Language { get; set; }

        [Display(Name = "Name")]
        public string WordName { get; set; }

        [Display(Name = "Info")]
        public string WordInformation { get; set; }
    }
}
