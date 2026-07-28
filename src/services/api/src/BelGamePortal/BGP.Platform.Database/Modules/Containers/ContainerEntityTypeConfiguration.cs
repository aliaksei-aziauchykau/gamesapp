using BGP.Platform.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Logging;

namespace BGP.Platform.Database.Modules.GameDefinitions;

public class ContainersEntityTypeConfiguration : IEntityTypeConfiguration<Container>
{
    public void Configure(EntityTypeBuilder<Container> builder)
    {
        builder
            // .Ignore(type => type.Id)
            .HasKey(type => type.ContainerId);

        builder
            .HasMany(type => type.ProjectContainers)
            .WithOne(type => type.Container)
            .HasForeignKey(type => type.ContainerId);

        builder
            .HasMany(type => type.Posts)
            .WithOne(type => type.Container)
            .HasForeignKey(type => type.ContainerId);

        builder
            .HasMany(type => type.Events)
            .WithOne(type => type.Container)
            .HasForeignKey(type => type.ContainerId);

        builder
            .Property(type => type.Name)
            .HasMaxLength(200);

        builder
            .ToTable("Containers");
    }
}
