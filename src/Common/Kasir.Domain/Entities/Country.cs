using Kasir.Domain.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Kasir.Domain.Entities
{
    public class Country : AuditableEntity
    {
        public Country()
        {
            CountryLanguages = new HashSet<CountryLanguage>();
            WordImages = new HashSet<WordImage>();
        }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(400)]
        public string ImagePath { get; set; }

        public ICollection<CountryLanguage> CountryLanguages { get; set; }

        public ICollection<WordImage> WordImages { get; set; }

    }
}
