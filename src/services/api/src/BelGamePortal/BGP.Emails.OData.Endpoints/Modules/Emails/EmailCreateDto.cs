using BGP.Emails.Domain.Entities;
using BGP.Shared.Libs.Mappings.Mappings;

namespace BGP.Emails.OData.Api.Modules.Emails;

public class EmailCreateDto : IMapTo<Email>
{
    public string Subject { get; init; } = default!;

    public string From { get; init; } = default!;

    public string To { get; init; } = default!;

    public string Body { get; init; } = default!;
}
