using BGP.Platform.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BGP.Platform.Database.Modules.GameDefinitions;

public class ParentContainerChildContainersEntityTypeConfiguration : IEntityTypeConfiguration<ParentContainerChildContainer>
{
    public void Configure(EntityTypeBuilder<ParentContainerChildContainer> builder)
    {
        builder
            // .Ignore(type => type.Id)
            .HasKey(type => new { type.ParentContainerId, type.ChildContainerId });

        builder
            .HasOne(type => type.ParentContainer)
            .WithMany(type => type.ParentContainers)
            .HasForeignKey(type => type.ParentContainerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(type => type.ChildContainer)
            .WithMany(type => type.ChildContainers)
            .HasForeignKey(type => type.ChildContainerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .Property(type => type.ParentContainerChildContainerId)
            .ValueGeneratedOnAdd();

        builder
            .ToTable("ParentContainerChildContainers");
    }
}
