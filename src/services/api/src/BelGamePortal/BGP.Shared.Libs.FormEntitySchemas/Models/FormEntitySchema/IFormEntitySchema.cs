using System.Collections.Generic;

namespace BGP.Shared.Libs.FormEntitySchemas.Models.EntityFormSchema;

public interface IFormEntitySchema
{
    string CollectionName { get; set; }

    string Title { get; set; }

    IDictionary<string, IControlFormEntitySchema> Controls { get; set; }
}
