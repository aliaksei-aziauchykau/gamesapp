using BGP.Platform.Application.Modules.Events;
using BGP.Platform.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Logging;

namespace BGP.Platform.Database.Modules.GameDefinitions;

public class EventsEntityTypeConfiguration : IEntityTypeConfiguration<Event>
{
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        builder
            // .Ignore(type => type.Id)
            .HasKey(type => type.EventId);

        builder
            .HasOne(type => type.Container)
            .WithMany(type => type.Events)
            .HasForeignKey(type => type.ContainerId);

        builder
            .Property(type => type.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder
            .Property(type => type.Description)
            .HasMaxLength(500);

        builder
            .Property(type => type.Location)
            .HasMaxLength(200);

        builder
            .Property(type => type.StartDate)
            .IsRequired();

        builder
            .Property(type => type.EndDate)
            .IsRequired();

        builder
            .Property(type => type.ViewComponent)
            .HasMaxLength(EventValidationRules.ViewComponentMaxLength);

        builder
            .ToTable("Events");
    }
}
