using Kasir.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kasir.Domain.Entities
{
    public class PushToken : AuditableEntity
    {
        [Required]
        [MaxLength(450)]
        public string Token { get; set; }

        [Required]
        public bool Valid { get; set; }

        public ICollection<PushTicket> PushTickets { get; set; }

    }
}
