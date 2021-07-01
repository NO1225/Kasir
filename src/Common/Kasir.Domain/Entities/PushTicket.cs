using Kasir.Domain.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kasir.Domain.Entities
{
    public class PushTicket : AuditableEntity
    {
        [Required]
        [MaxLength(450)]
        public string ReceiptId { get; set; }


        [ForeignKey(nameof(PushToken))]
        [Required]
        public int PushTokenId { get; set; }

        public PushToken PushToken { get; set; }
    }
}
