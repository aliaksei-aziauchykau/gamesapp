using BGP.Platform.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Logging;

namespace BGP.Platform.Database.Modules.GameDefinitions;

public class ProjectsEntityTypeConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder
            // .Ignore(type => type.Id)
            .HasKey(type => type.ProjectId);

        builder
            .HasOne(type => type.Workspace)
            .WithMany(type => type.Projects)
            .HasForeignKey(type => type.WorkspaceId);

        builder
            .HasOne(type => type.ProjectVisibility)
            .WithMany(type => type.Projects)
            .IsRequired(false);

        builder
            .ToTable("Projects");
    }
}
