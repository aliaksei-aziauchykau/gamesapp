using BGP.Games.Domain.Entities.Games.Kroko;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Logging;

namespace BGP.Games.Database.Modules.Kroko;

public class KrokoWordConfiguration : IEntityTypeConfiguration<KrokoWord>
{
    public void Configure(EntityTypeBuilder<KrokoWord> builder)
    {
        builder.HasKey(type => type.Id);

        builder
           .HasOne(type => type.Language)
           .WithMany(type => type.Words)
           .HasForeignKey(type => type.LanguageId);

        builder
           .Property(type => type.Word)
           .HasMaxLength(50);

        builder
            .Property(type => type.Meaning)
            .HasMaxLength(200);

        builder.ToTable("KrokoWord");
    }
}
