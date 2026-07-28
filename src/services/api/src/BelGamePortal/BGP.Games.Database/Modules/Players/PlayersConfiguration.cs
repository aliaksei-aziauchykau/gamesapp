using BGP.Games.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Logging;

namespace BGP.Emails.Database.Modules.GameDefinitions;

public class PlayersConfiguration : IEntityTypeConfiguration<Player>
{
    private readonly ILogger<PlayersConfiguration> logger;

    public PlayersConfiguration(ILogger<PlayersConfiguration> logger)
    {
        this.logger = logger;
    }

    public void Configure(EntityTypeBuilder<Player> builder)
    {
        builder.HasKey(type => type.Id);

        builder
            .HasOne(type => type.Game)
            .WithMany(type => type.Players)
            .HasForeignKey(type => type.GameId);

        builder
            .Navigation(builder => builder.Game);

        builder
            .Property(type => type.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder
            .ToTable("Players");
    }
}
