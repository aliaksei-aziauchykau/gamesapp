using BGP.Games.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Logging;

namespace BGP.Emails.Database.Modules.GameDefinitions;

public class GameDefinitionsConfiguration : IEntityTypeConfiguration<GameDefinition>
{
    private readonly ILogger<GameDefinitionsConfiguration> logger;

    public GameDefinitionsConfiguration(ILogger<GameDefinitionsConfiguration> logger)
    {
        this.logger = logger;
    }

    public void Configure(EntityTypeBuilder<GameDefinition> builder)
    {
        builder.HasKey(type => type.Id);

        builder
            .HasMany(type => type.Games)
            .WithOne(type => type.GameDefinition)
            .HasForeignKey(type => type.GameDefinitionId);

        builder
          .Property(type => type.Name)
          .HasMaxLength(200)
          .IsRequired();

        builder
            .Property(type => type.Description)
            .HasMaxLength(500)
            .IsRequired();

        builder
            .Property(type => type.ImageUrl)
            .HasMaxLength(100);

        builder
            .Property(type => type.ImageAlt)
            .HasMaxLength(100);

        builder
            .Property(type => type.GamePageAppUrl)
            .HasMaxLength(100);

        builder
            .ToTable("GameDefinitions");
    }
}
