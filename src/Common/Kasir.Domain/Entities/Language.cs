using Kasir.Domain.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Kasir.Domain.Entities
{
    public class Language : AuditableEntity
    {
        public Language()
        {
            WordLanguages = new HashSet<WordLanguage>();
            CountryLanguages = new HashSet<CountryLanguage>();
        }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(200)]
        public string ImageName { get; set; }

        public ICollection<WordLanguage> WordLanguages { get; set; }

        public ICollection<CountryLanguage> CountryLanguages { get; set; }

    }
}
