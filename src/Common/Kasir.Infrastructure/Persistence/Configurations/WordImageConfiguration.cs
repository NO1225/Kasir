using Kasir.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kasir.Infrastructure.Persistence.Configurations
{
    public class WordImageConfiguration : IEntityTypeConfiguration<WordImage>
    {
        public void Configure(EntityTypeBuilder<WordImage> builder)
        {
            builder.HasOne<Word>(cl=>cl.Word).WithMany(c=>c.WordImages).HasForeignKey(c => c.WordId);
            builder.HasOne<Country>(cl=>cl.Country).WithMany(c=>c.WordImages).HasForeignKey(c => c.CountryId);
        }
    }

}
