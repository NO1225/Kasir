using Kasir.Domain.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Kasir.Domain.Entities
{
    public class Announcement : AuditableEntity, IHasDomainEvent
    {
        public Announcement()
        {
            DomainEvents = new List<DomainEvent>();
        }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        [MaxLength(500)]
        public string Body { get; set; }

        public List<DomainEvent> DomainEvents { get; set; }
    }
}
