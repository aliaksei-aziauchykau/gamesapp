

using BGP.Games.Domain.Entities;
using BGP.Shared.Libs.FormEntitySchemas.Interfaces;
using BGP.Shared.Libs.FormEntitySchemas.Models.EntityFormSchema.Controls;
using BGP.Shared.Libs.FormEntitySchemas.Services;

namespace BGP.Games.Application.Modules.Games;

public class GameFormEntitySchemaConfiguration : FormEntitySchemaConfiguration<Game>, IFormEntitySchemaConfiguration<Game>
{
    public override Task ConfigureAsync(IFormEntitySchemaBuilder builder)
    {
        var nameControlSchema = new InputControlFormEntitySchema { Name = "name" };
        var descriptionControlSchema = new InputControlFormEntitySchema { Name = "description" };
        var isSingleControlSchema = new CheckboxControlFormEntitySchema { Name = "isSingle" };
        var addressControlSchema = new InputControlFormEntitySchema { Name = "address" };
        var isHomelessControlSchema = new InputControlFormEntitySchema { Name = "isHomeless" };

        builder
            .WithCollectionName("Games")
            .WithTitle("Game")
            .AddControlSchema(nameControlSchema)
            .AddControlSchema(descriptionControlSchema)
            .AddControlSchema(isSingleControlSchema)
            .AddControlSchema(addressControlSchema)
            .AddControlSchema(isHomelessControlSchema);

        return Task.CompletedTask;
    }
}
