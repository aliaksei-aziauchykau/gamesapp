
using BGP.Shared.Libs.Domain.Entities;
using BGP.Shared.Libs.Mappings.Mappings;
using BGP.Shared.Modules.EntityApi.OData;

namespace BGP.Shared.Modules.EntityApi;

public interface IEntityApiClient<TEntity, TKey>
{
    Task<IList<TEntity>> GetAllAsync(ODataQuery query, CancellationToken cancellationToken = default);

    Task<IList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<TEntity> GetByIdAsync(TKey id, CancellationToken cancellationToken = default);

    Task<TEntity> GetByIdAsync(TKey id, ODataQuery query, CancellationToken cancellationToken = default);

    Task<TEntity> AddAsync<TEntityCreateDto>(TEntityCreateDto entity, CancellationToken cancellationToken = default)
           where TEntityCreateDto : IMapTo<TEntity>;

    Task<TEntity> UpdateAsync<TEntityUpdateDto>(TEntityUpdateDto entity, CancellationToken cancellationToken = default)
         where TEntityUpdateDto : IMapTo<TEntity>, IHasKey<TKey>;

    Task<TEntity> PatchAsync<TEntityPatchDto>(TEntityPatchDto entity, CancellationToken cancellationToken = default)
        where TEntityPatchDto : IMapTo<TEntity>, IHasKey<TKey>;

    Task<TEntity> DeleteAsync(TKey id, CancellationToken cancellationToken = default);
}
