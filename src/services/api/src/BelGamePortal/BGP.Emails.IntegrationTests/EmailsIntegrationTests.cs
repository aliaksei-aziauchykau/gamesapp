using BGP.Emails.Database;
using BGP.Emails.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BGP.Emails.IntegrationTests;

public class EmailsDatabaseIntegrationTests : IClassFixture<DatabaseFixture<EmailDbContext>>
{
    private readonly DatabaseFixture<EmailDbContext> _fixture;

    public EmailsDatabaseIntegrationTests(DatabaseFixture<EmailDbContext> fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task EmailsDbContext_ShouldSaveEmail_Async()
    {
        // Arrange
        var email = new Email
        {
            Subject = "Welcome to the Game Portal",
            Body = "Welcome to the Game Portal. We are excited to have you here.",
            From = "test@source.com",
            To = "test@destination.com"
        };

        // Act
        _fixture.Context.Emails.Add(email);
        await _fixture.Context.SaveChangesAsync();

        // Detach the entity to simulate a new context (optional)
        _fixture.Context.Entry(email).State = EntityState.Detached;

        // Retrieve the entity from the database
        var emailSaved = await _fixture.Context.Emails.FirstOrDefaultAsync(e => e != null);

        // Assert
        Assert.NotNull(emailSaved);
        Assert.Equal(email.Subject, emailSaved.Subject);
        Assert.Equal(email.Body, emailSaved.Body);
        Assert.Equal(email.From, emailSaved.From);
        Assert.Equal(email.To, emailSaved.To);
    }
}
