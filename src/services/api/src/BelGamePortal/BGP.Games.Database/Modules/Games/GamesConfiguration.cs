using BGP.Games.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Logging;

namespace BGP.Games.Database.Modules.Games;

public class GamesConfiguration : IEntityTypeConfiguration<Game>
{
    private readonly ILogger<GamesConfiguration> logger;

    public GamesConfiguration(ILogger<GamesConfiguration> logger)
    {
        this.logger = logger;
    }

    public void Configure(EntityTypeBuilder<Game> builder)
    {
        builder.HasKey(type => type.Id);

        builder
            .HasMany(type => type.Players)
            .WithOne(type => type.Game)
            .HasForeignKey(type => type.GameId);

        builder
           .HasOne(type => type.GameDefinition)
           .WithMany(type => type.Games)
           .HasForeignKey(type => type.GameDefinitionId);

        builder
            .Property(type => type.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder
           .Property(type => type.Code);

        builder.ToTable("Games");
    }
}
