using BGP.Shared.Libs.Database.DbContexts;
using BGP.Shared.Libs.Domain.Entities;
using BGP.Shared.Modules.CrudCommands.Repositories;

namespace BGP.Shared.Modules.CrudCommands.Services;

public class CrudService<TEntity, TKey> : ICrudService<TEntity, TKey>
    where TEntity : class, IHasKey<TKey>
{
    protected readonly IRepository<TEntity, TKey> _repository;

    private IUnitOfWork _unitOfWork => _repository.UnitOfWork;

    public CrudService(IRepository<TEntity, TKey> repository)
    {
        _repository = repository;
    }

    public virtual Task<IList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return _repository.GetAllAsync(cancellationToken);
    }

    public virtual Task<TEntity?> GetByIdAsync(TKey id, CancellationToken cancellationToken = default)
    {
        return _repository.GetByIdAsync(id, cancellationToken);
    }

    public virtual Task<IQueryable<TEntity?>> GetByIdQueryAsync(TKey id, CancellationToken cancellationToken = default)
    {
        return _repository.GetByIdQueryAsync(id, cancellationToken);
    }

    public virtual async Task<TEntity> AddOrUpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        if (entity.Id.Equals(default))
        {
            return await AddAsync(entity, cancellationToken);
        }

        return await UpdateAsync(entity, cancellationToken);
    }

    public virtual async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        var entityToReturn = await _repository.AddAsync(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return entityToReturn;
    }

    public virtual async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        var entityToReturn = await _repository.UpdateAsync(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return entityToReturn;
    }

    public virtual async Task<TEntity> DeleteAsync(TKey id, CancellationToken cancellationToken = default)
    {
        var entityToReturn = await _repository.DeleteAsync(id);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return entityToReturn;
    }

    public virtual Task<IQueryable<TEntity>> GetAsync(CancellationToken cancellationToken = default)
    {
        return _repository.GetAsync(cancellationToken);
    }

    public virtual async Task<TEntity> PatchAsync(TKey id, IDelta<TEntity> entity, CancellationToken cancellationToken = default)
    {
        var entityToReturn = await _repository.PatchAsync(id, entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return entityToReturn;
    }
}

