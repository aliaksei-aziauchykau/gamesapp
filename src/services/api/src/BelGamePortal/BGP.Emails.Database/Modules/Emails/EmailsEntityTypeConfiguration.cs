using BGP.Emails.Domain.Entities;
using BGP.Shared.Libs.Database.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Logging;

namespace BGP.Emails.Database.Modules.Emails;

public class EmailsEntityTypeConfiguration : IEntityTypeConfiguration<Email>
{
    public void Configure(EntityTypeBuilder<Email> builder)
    {
        builder.HasKey(type => type.Id);

        builder
            .Property(type => type.From)
            .HasMaxLength(100)
            .IsRequired();

        builder
           .Property(type => type.To)
           .HasMaxLength(100)
           .IsRequired();

        builder
           .Property(type => type.Subject)
           .HasMaxLength(200);

        builder
           .Property(type => type.Body)
           .IsRequired();

        builder.ToTable("Emails");
    }
}
