using BGP.Shared.Libs.FormEntitySchemas.Models.EntityFormSchema;

namespace BGP.Shared.Libs.FormEntitySchemas.Services;

public interface IFormEntitySchemaBuilder
{
    IFormEntitySchema FormEntitySchema { get; }

    IFormEntitySchemaBuilder CreateFormSchema();

    IFormEntitySchemaBuilder WithCollectionName(string collectionName);

    IFormEntitySchemaBuilder WithTitle(string title);

    IFormEntitySchemaBuilder AddControlSchema(IControlFormEntitySchema controlSchema);

    IFormEntitySchemaBuilder AddControlSchema(string controlName, IControlFormEntitySchema controlSchema);
}
