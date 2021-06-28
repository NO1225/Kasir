using System.ComponentModel.DataAnnotations;

namespace Kasir.Api.ViewModel
{
    public class WordImageViewModel
    {
        public int CountryId { get; set; }

        [Display(Name = "Country")]
        public string Country { get; set; }

        [Display(Name = "Image")]
        public string ImagePath { get; set; }

    }
}
