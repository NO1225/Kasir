using Kasir.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace Kasir.Domain.Entities
{
    public class WordCountry : AuditableEntity
    {
        public Word Word { get; set; }

        [Required]
        public int WordId { get; set; }

        public Country Country { get; set; }

        [Required]
        public int CountryId { get; set; }
    }
}
