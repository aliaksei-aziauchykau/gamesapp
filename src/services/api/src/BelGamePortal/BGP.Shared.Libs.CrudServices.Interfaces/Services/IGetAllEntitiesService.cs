

namespace BGP.Shared.Libs.CrudServices.Interfaces.Services;

public interface IGetAllEntitiesService<TEntity>
{
    Task<IList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
}
