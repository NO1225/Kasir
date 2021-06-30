using Kasir.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kasir.Infrastructure.Persistence.Configurations
{
    public class AppInfoLanguageConfiguration : IEntityTypeConfiguration<AppInfoLanguage>
    {
        public void Configure(EntityTypeBuilder<AppInfoLanguage> builder)
        {
            builder.HasOne<AppInfo>(cl => cl.AppInfo).WithMany(c => c.AppInfoLanguages).HasForeignKey(c => c.AppInfoId);
            builder.HasOne<Language>(cl => cl.Language).WithMany(c => c.AppInfoLanguages).HasForeignKey(c => c.LanguageId);
        }
    }

}
