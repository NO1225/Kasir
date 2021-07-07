using System.ComponentModel.DataAnnotations;

namespace Kasir.Api.ViewModel
{
    public class WordCountryViewModel
    {
        public int CountryId { get; set; }

        [Display(Name = "البلد")]
        public string Country { get; set; }

        [Display(Name = "مضمنة")]
        public bool Checked { get; set; }
        public int Id { get; set; }
    }
}
