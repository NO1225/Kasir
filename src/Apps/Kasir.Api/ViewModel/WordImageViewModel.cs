using System.ComponentModel.DataAnnotations;

namespace Kasir.Api.ViewModel
{
    public class WordImageViewModel
    {
        public int CountryId { get; set; }

        [Display(Name = "البلد")]
        public string Country { get; set; }

        [Display(Name = "الصورة")]
        public string ImagePath { get; set; }
        public int Id { get; internal set; }
    }
}
