using BGP.Platform.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Logging;

namespace BGP.Platform.Database.Modules.GameDefinitions;

public class ProjectVisibilitiesEntityTypeConfiguration : IEntityTypeConfiguration<ProjectVisibility>
{
    public void Configure(EntityTypeBuilder<ProjectVisibility> builder)
    {
        builder
            // .Ignore(type => type.Id)
            .HasKey(type => type.ProjectVisibilityId);

        builder
            .HasMany(type => type.Projects)
            .WithOne(project => project.ProjectVisibility)
            .HasForeignKey(project => project.ProjectVisibilityId);

        builder
            .Property(type => type.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder
            .Property(type => type.Description)
            .HasMaxLength(500)
            .IsRequired();

        builder
            .ToTable("ProjectVisibilities");
    }
}
