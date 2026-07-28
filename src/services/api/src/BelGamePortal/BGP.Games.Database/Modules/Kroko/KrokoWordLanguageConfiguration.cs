using BGP.Games.Domain.Entities.Games.Kroko;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BGP.Games.Database.Modules.Kroko;

public class KrokoWordLanguageConfiguration : IEntityTypeConfiguration<KrokoWordLanguage>
{
    public void Configure(EntityTypeBuilder<KrokoWordLanguage> builder)
    {
        builder
           .HasMany(type => type.Words)
           .WithOne(type => type.Language)
           .HasForeignKey(type => type.LanguageId);

        builder.HasKey(type => type.Id);

        builder
            .Property(type => type.Code)
            .HasMaxLength(25);

        builder
            .Property(type => type.Name)
            .HasMaxLength(50);

        builder
            .ToTable("KrokoWordLanguage");
    }
}
