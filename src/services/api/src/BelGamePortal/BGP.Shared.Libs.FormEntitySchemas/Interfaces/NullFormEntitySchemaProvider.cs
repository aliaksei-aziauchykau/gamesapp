using System;
using System.Threading.Tasks;
using BGP.Shared.Libs.FormEntitySchemas.Models.EntityFormSchema;
using BGP.Shared.Libs.FormEntitySchemas.Services;

namespace BGP.Shared.Libs.FormEntitySchemas.Interfaces;
public class NullFormEntitySchemaProvider<T> : IFormEntitySchemaConfiguration<T> where T : class
{
    public IFormEntitySchema FormEntitySchema => throw new NotImplementedException();

    public Task<IFormEntitySchemaConfiguration<T>> ConfigureAsync()
    {
        throw new NotImplementedException();
    }

    public virtual Task ConfigureAsync(IFormEntitySchemaBuilder builder)
    {
        throw new NotImplementedException();
    }
}
