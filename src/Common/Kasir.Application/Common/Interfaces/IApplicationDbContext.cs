using System.Threading;
using System.Threading.Tasks;
using Kasir.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kasir.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Country> Countries { get; set; }

        DbSet<CountryLanguage> CountryLanguages { get; set; }

        DbSet<Language> Languages { get; set; }

        DbSet<Word> Words { get; set; }

        DbSet<WordImage> WordImages { get; set; }

        DbSet<WordLanguage> WordLanguages { get; set; }

        DbSet<AppInfo> AppInfos { get; set; }

        DbSet<AppInfoLanguage> AppInfoLanguages { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
