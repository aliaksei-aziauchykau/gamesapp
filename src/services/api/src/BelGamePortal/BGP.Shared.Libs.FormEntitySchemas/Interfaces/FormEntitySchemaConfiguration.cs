using BGP.Shared.Libs.FormEntitySchemas.Models.EntityFormSchema;
using BGP.Shared.Libs.FormEntitySchemas.Services;

namespace BGP.Shared.Libs.FormEntitySchemas.Interfaces;
public abstract class FormEntitySchemaConfiguration<T> : IFormEntitySchemaConfiguration<T> where T : class
{
    private readonly IFormEntitySchemaBuilder builder = new FormEntitySchemaBuilder();

    public IFormEntitySchema FormEntitySchema => builder.FormEntitySchema;

    public async Task<IFormEntitySchemaConfiguration<T>> ConfigureAsync()
    {
        this.builder.CreateFormSchema();
        await this.ConfigureAsync(this.builder);
        return this;
    }

    public abstract Task ConfigureAsync(IFormEntitySchemaBuilder builder);
}
