using Kasir.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace Kasir.Domain.Entities
{
    public class CountryLanguage : AuditableEntity
    {
        public Country Country { get; set; }

        [Required]
        public int CountryId { get; set; }

        public Language Language { get; set; }

        [Required]
        public int LanguageId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

    }
}
