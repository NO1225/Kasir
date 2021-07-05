using Kasir.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace Kasir.Domain.Entities
{
    public class AppInfoLanguage : AuditableEntity
    {
        public AppInfo AppInfo { get; set; }

        [Required]
        public int AppInfoId { get; set; }

        public Language Language { get; set; }

        [Required]
        public int LanguageId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        [MaxLength(450)]
        public string Welcome { get; set; }

        [Required]
        [MaxLength(100)]
        public string Disclaimer { get; set; }

        [Required]
        [MaxLength(450)]
        public string Description { get; set; }

    }
}
