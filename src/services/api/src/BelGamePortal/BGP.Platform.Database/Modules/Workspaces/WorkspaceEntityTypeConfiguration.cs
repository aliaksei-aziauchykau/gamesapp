using BGP.Platform.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Logging;

namespace BGP.Platform.Database.Modules.GameDefinitions;

public class WorkspacesEntityTypeConfiguration : IEntityTypeConfiguration<Workspace>
{
    public void Configure(EntityTypeBuilder<Workspace> builder)
    {
        builder
            // .Ignore(type => type.Id)
            .HasKey(type => type.WorkspaceId);

        builder
            .HasMany(type => type.Projects)
            .WithOne(type => type.Workspace)
            .HasForeignKey(type => type.WorkspaceId);

        builder
            .Property(type => type.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder
            .Property(type => type.OwnerUserId);

        builder
            .ToTable("Workspaces");
    }
}
