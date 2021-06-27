using System.ComponentModel.DataAnnotations;

namespace Kasir.Application.Dto
{
    public class CountryLanguageDto 
    {
        public int LanguageId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        
    }
}
