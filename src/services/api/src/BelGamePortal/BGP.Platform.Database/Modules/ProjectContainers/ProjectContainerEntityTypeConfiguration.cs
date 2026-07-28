using BGP.Platform.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Logging;

namespace BGP.Platform.Database.Modules.GameDefinitions;

public class ProjectContainersEntityTypeConfiguration : IEntityTypeConfiguration<ProjectContainer>
{
    public void Configure(EntityTypeBuilder<ProjectContainer> builder)
    {
        builder
            // .Ignore(type => type.Id)
            .HasKey(type => new { type.ProjectId, type.ContainerId });

        builder
            .HasOne(type => type.Project)
            .WithMany(type => type.ProjectContainers)
            .HasForeignKey(type => type.ProjectId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(type => type.Container)
            .WithMany(type => type.ProjectContainers)
            .HasForeignKey(type => type.ContainerId)
             .OnDelete(DeleteBehavior.Restrict);

        builder
            .Property(pf => pf.ProjectContainerId)
            .ValueGeneratedOnAdd();

        builder
            .ToTable("ProjectContainers");
    }
}
