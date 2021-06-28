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
            WordImages = new HashSet<WordImage>();
        }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(450)]
        public string Information { get; set; }

        [Required]
        [MaxLength(400)]
        public string ImageName { get; set; }

        public ICollection<WordLanguage> WordLanguages { get; set; }

        public ICollection<WordImage> WordImages { get; set; }
    }
}
