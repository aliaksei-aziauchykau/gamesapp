using System.Text.Json.Serialization;
using BGP.Emails.Domain.Entities;
using BGP.Shared.Libs.Mappings.Mappings;

namespace BGP.Emails.OData.Endpoints.Modules.Emails;

public class EmailPatchDto : IMapToDelta<Email>
{
    public string? Subject { get; init; } = default!;

    public string? To { get; init; } = default!;
}
