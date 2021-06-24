using Kasir.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kasir.Infrastructure.Persistence.Configurations
{
    public class WordLanguageConfiguration : IEntityTypeConfiguration<WordLanguage>
    {
        public void Configure(EntityTypeBuilder<WordLanguage> builder)
        {
            builder.HasOne<Word>(cl=>cl.Word).WithMany(c=>c.WordLanguages).HasForeignKey(c => c.WordId);
            builder.HasOne<Language>(cl=>cl.Language).WithMany(c=>c.WordLanguages).HasForeignKey(c => c.LanguageId);
        }
    }

}
