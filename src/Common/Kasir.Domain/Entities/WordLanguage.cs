using Kasir.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace Kasir.Domain.Entities
{
    public class WordLanguage : AuditableEntity
    {
        public Word Word { get; set; }

        [Required]
        public int WordId { get; set; }

        public Language Language { get; set; }

        [Required]
        public int LanguageId { get; set; }
                
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        [MaxLength(450)]
        public string Information { get; set; }
    }
}
