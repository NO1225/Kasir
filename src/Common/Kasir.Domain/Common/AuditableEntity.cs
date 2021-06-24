using System;
using System.ComponentModel.DataAnnotations;

namespace Kasir.Domain.Common
{
    public abstract class AuditableEntity
    {
        [Key]
        public int Id { get; set; }

        public string Creator { get; set; }

        public DateTime CreateDate { get; set; }

        public string Modifier { get; set; }

        public DateTime? ModifyDate { get; set; }

    }
}
