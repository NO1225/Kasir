using Kasir.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kasir.Infrastructure.Persistence.Configurations
{
    public class WordCountryConfiguration : IEntityTypeConfiguration<WordCountry>
    {
        public void Configure(EntityTypeBuilder<WordCountry> builder)
        {
            builder.HasOne<Word>(cl=>cl.Word).WithMany(c=>c.WordCountries).HasForeignKey(c => c.WordId);
            builder.HasOne<Country>(cl=>cl.Country).WithMany(c=>c.WordCountries).HasForeignKey(c => c.CountryId);
        }
    }

}
