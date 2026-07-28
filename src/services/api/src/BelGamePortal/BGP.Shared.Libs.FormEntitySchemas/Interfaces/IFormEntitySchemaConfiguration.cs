using BGP.Shared.Libs.FormEntitySchemas.Models.EntityFormSchema;

namespace BGP.Shared.Libs.FormEntitySchemas.Interfaces;

public interface IFormEntitySchemaConfiguration<T> where T : class
{
    IFormEntitySchema FormEntitySchema { get; }

    Task<IFormEntitySchemaConfiguration<T>> ConfigureAsync();
}
