using Kasir.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace Kasir.Domain.Entities
{
    public class WordImage : AuditableEntity
    {
        public Word Word { get; set; }

        [Required]
        public int WordId { get; set; }

        public Country Country { get; set; }

        [Required]
        public int CountryId { get; set; }

        [Required]
        [MaxLength(400)]
        public string ImageName { get; set; }
    }
}
