using Kasir.Domain.Common;
using System.Collections.Generic;

namespace Kasir.Domain.Entities
{
    public class AppInfo : AuditableEntity
    {
        public AppInfo()
        {
            AppInfoLanguages = new List<AppInfoLanguage>();
        }
        public ICollection<AppInfoLanguage> AppInfoLanguages { get; set; }
    }
}
