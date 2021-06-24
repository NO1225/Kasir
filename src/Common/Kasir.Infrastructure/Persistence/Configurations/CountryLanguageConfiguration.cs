using Kasir.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kasir.Infrastructure.Persistence.Configurations
{
    public class CountryLanguageConfiguration : IEntityTypeConfiguration<CountryLanguage>
    {
        public void Configure(EntityTypeBuilder<CountryLanguage> builder)
        {
            builder.HasOne<Country>(cl=>cl.Country).WithMany(c=>c.CountryLanguages).HasForeignKey(c => c.CountryId);
            builder.HasOne<Language>(cl=>cl.Language).WithMany(c=>c.CountryLanguages).HasForeignKey(c => c.LanguageId);
        }
    }

}
