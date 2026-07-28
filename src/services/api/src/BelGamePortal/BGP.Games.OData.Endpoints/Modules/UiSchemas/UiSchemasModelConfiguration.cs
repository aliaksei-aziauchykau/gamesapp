
using Asp.Versioning;
using Asp.Versioning.OData;
using BGP.Games.Domain.Entities;
using BGP.Shared.Libs.FormEntitySchemas.Models.EntityFormSchema;
using BGP.Shared.Libs.FormEntitySchemas.Models.EntityFormSchema.Controls;
using Microsoft.OData.ModelBuilder;

namespace BGP.Games.OData.Endpoints.Modules.Games;


public class UiSchema
{
    public required string CollectionName { get; init; }
}

public class UiSchemasModelConfiguration : IModelConfiguration
{
    public void Apply(ODataModelBuilder builder, ApiVersion apiVersion, string? routePrefix)
    {
        var set = builder.EntitySet<UiSchema>("GamesUiSchemas");
        var type = set.EntityType;

        type.HasKey(x => x.CollectionName);

        var function = type.Collection.Function("GetUiSchema");
        function.Parameter<string>("collectionSchemaName");
        function.Returns<int>();
        // type.Select().Expand().Filter().Count();
        // type.ComplexProperty(x => x.Controls);
        // // builder.AddComplexType(typeof(IControlFormEntitySchema));
        // // builder.AddEntityType(typeof(IControlFormEntitySchema));
        // builder.AddComplexType(typeof(IDictionary<string, IControlFormEntitySchema>));
        // builder.AddEntityType(typeof(FormEntitySchema));
        builder.AddEntityType(typeof(SelectControlFormEntitySchema<string>));
        builder.AddEntityType(typeof(InputControlFormEntitySchema));
        builder.AddEntityType(typeof(RteControlFormEntitySchema));
        builder.AddEntityType(typeof(RadioControlFormEntitySchema));
    }
}
