using BGP.Shared.Libs.Domain.Entities;
using Microsoft.AspNetCore.OData.Deltas;

namespace BGP.Shared.Modules.OData.Crud.Api.Helpers.DeltaAdapter;

public class DeltaAdapter<TEntity> : IDelta<TEntity> where TEntity : class
{
    private readonly Delta<TEntity> delta;

    public DeltaAdapter(Delta<TEntity> delta)
    {
        this.delta = delta;
    }

    public TEntity Patch(TEntity entity)
    {
        return delta.Patch(entity);
    }
}

public static class DeltaExtensions
{
    public static IDelta<TEntity> ToIDelta<TEntity>(this Delta<TEntity> delta) where TEntity : class
    {
        return new DeltaAdapter<TEntity>(delta);
    }
}
