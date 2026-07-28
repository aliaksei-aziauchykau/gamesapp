

using BGP.Games.Domain.Entities;
using BGP.Shared.Libs.FormEntitySchemas.Interfaces;
using BGP.Shared.Libs.FormEntitySchemas.Models.EntityFormSchema.Controls;
using BGP.Shared.Libs.FormEntitySchemas.Services;

namespace BGP.Games.Application.Modules.Posts;

public class PostsFormEntitySchemaConfiguration : FormEntitySchemaConfiguration<Post>, IFormEntitySchemaConfiguration<Post>
{
    public override Task ConfigureAsync(IFormEntitySchemaBuilder builder)
    {
        var idControlSchema = new InputControlFormEntitySchema { Name = "id", Label = "Id" };
        var titleControlSchema = new InputControlFormEntitySchema { Name = "title", Label = "Title", Placeholder = "Type Title of your post" };
        var contentControlSchema = new RteControlFormEntitySchema { Name = "content" };

        builder
            .WithCollectionName("Posts")
            .WithTitle("Post")
            .AddControlSchema(idControlSchema)
            .AddControlSchema(titleControlSchema)
            .AddControlSchema(contentControlSchema);

        return Task.CompletedTask;
    }
}
