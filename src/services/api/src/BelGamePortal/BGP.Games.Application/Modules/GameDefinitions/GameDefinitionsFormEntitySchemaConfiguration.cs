

using BGP.Games.Domain.Entities;
using BGP.Shared.Libs.FormEntitySchemas.Interfaces;
using BGP.Shared.Libs.FormEntitySchemas.Models.EntityFormSchema.Controls;
using BGP.Shared.Libs.FormEntitySchemas.Services;

namespace BGP.Games.Application.Modules.GameDefinitions;

public class GameDefinitionsFormEntitySchemaConfiguration : FormEntitySchemaConfiguration<GameDefinition>, IFormEntitySchemaConfiguration<GameDefinition>
{
    public override Task ConfigureAsync(IFormEntitySchemaBuilder builder)
    {
        var nameControlSchema = new InputControlFormEntitySchema
        {
            Name = "name",
            Label = "Name",
            Placeholder = "John"
        };

        var descriptionControlSchema = new InputControlFormEntitySchema
        {
            Name = "description",
            Label = "Description",
            Placeholder = ""
        };

        var imageAltControlSchema = new InputControlFormEntitySchema
        {
            Name = "imageAlt",
            Label = "ImageAlt"
        };

        builder
            .WithCollectionName("GameDefinitions")
            .AddControlSchema(nameControlSchema)
            .AddControlSchema(descriptionControlSchema)
            .AddControlSchema(imageAltControlSchema);

        return Task.CompletedTask;
    }
}
