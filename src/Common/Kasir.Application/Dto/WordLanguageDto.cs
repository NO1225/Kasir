using System.ComponentModel.DataAnnotations;

namespace Kasir.Application.Dto
{
    public class WordLanguageDto
    {
        public int LanguageId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        [MaxLength(450)]
        public string Information { get; set; }
    }
}
