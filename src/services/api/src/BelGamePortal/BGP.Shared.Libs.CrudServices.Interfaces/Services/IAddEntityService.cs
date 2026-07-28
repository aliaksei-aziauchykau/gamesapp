namespace BGP.Shared.Libs.CrudServices.Interfaces.Services;

public interface IAddEntityService<TEntity> where TEntity : class
{
    Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);

}
