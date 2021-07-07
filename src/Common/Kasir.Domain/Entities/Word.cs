using Kasir.Domain.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Kasir.Domain.Entities
{
    public class Word : AuditableEntity
    {
        public Word()
        {
            WordLanguages = new HashSet<WordLanguage>();
            WordCountries = new HashSet<WordCountry>();
        }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(400)]
        public string ImageName { get; set; }

        public ICollection<WordLanguage> WordLanguages { get; set; }

        public ICollection<WordCountry> WordCountries { get; set; }
    }
}
