using System.Collections.Generic;

namespace BGP.Shared.Libs.FormEntitySchemas.Models.EntityFormSchema;
public class FormEntitySchema : IFormEntitySchema
{
    public string CollectionName { get; set; }

    public string? Title { get; set; }

    public IDictionary<string, IControlFormEntitySchema> Controls { get; set; } = new Dictionary<string, IControlFormEntitySchema>();
}
