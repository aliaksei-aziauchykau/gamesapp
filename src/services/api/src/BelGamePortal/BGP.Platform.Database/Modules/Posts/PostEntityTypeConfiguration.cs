using BGP.Platform.Application.Modules.Posts;
using BGP.Platform.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Logging;

namespace BGP.Platform.Database.Modules.GameDefinitions;

public class PostsEntityTypeConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder
            // .Ignore(type => type.Id)
            .HasKey(type => type.PostId);

        builder
            .HasOne(type => type.Container)
            .WithMany(type => type.Posts)
            .HasForeignKey(type => type.ContainerId);

        builder
            .Property(type => type.Title)
            .HasMaxLength(PostValidationRules.TitleMaxLength)
            .IsRequired();

        builder
            .Property(type => type.Content)
            .HasMaxLength(PostValidationRules.ContentMaxLength);

        builder
            .Property(type => type.ViewComponent)
            .HasMaxLength(PostValidationRules.ViewComponentMaxLength);

        builder
            .ToTable("Posts");
    }
}
