using AutoMapper;
using BGP.Shared.Libs.Database.DbContexts;
using BGP.Shared.Libs.Domain.Entities;
using BGP.Shared.Modules.CrudCommands.DataSources;
using BGP.Shared.Modules.CrudCommands.Exceptions;
using BGP.Shared.Libs.Common.Helpers;
using Microsoft.EntityFrameworkCore;

namespace BGP.Shared.Modules.CrudCommands.Repositories;

public class Repository<TEntity, TKey> : IRepository<TEntity, TKey>
    where TEntity : class, IHasKey<TKey>
{
    private readonly IDataSource<TEntity, TKey> _dataSource;

    public IUnitOfWork UnitOfWork => _dataSource.UnitOfWork;

    private readonly IMapper _mapper;

    public Repository(IDataSource<TEntity, TKey> dataSource)
    {
        _dataSource = dataSource;
    }

    public async Task<IList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var entities = await _dataSource.GetAllAsync(cancellationToken);

        return entities;
    }

    public async Task<TEntity> GetByIdAsync(TKey id, CancellationToken cancellationToken = default)
    {
        var entity = await _dataSource.GetByIdAsync(id, cancellationToken);

        if (entity != null) return entity;

        throw new NotFoundException($"Entity with id:{id} is not found");
    }

    public async Task<IQueryable<TEntity?>> GetByIdQueryAsync(TKey id, CancellationToken cancellationToken = default)
    {
        var query = await _dataSource.GetByIdQueryAsync(id, cancellationToken);

        return query;
    }

    public async Task<TEntity> AddOrUpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        if (entity.Id.Equals(default(TKey)))
        {
            return await AddAsync(entity, cancellationToken);
        }

        return await UpdateAsync(entity, cancellationToken);

    }

    public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await _dataSource.AddAsync(entity, cancellationToken);
        return entity;
    }

    public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await GetByIdAsync(entity.Id, cancellationToken);

        await _dataSource.UpdateAsync(entity, cancellationToken);

        return entity;
    }

    public async Task<TEntity> DeleteAsync(TKey id, CancellationToken cancellationToken = default)
    {
        var entity = await GetByIdAsync(id, cancellationToken);

        await _dataSource.DeleteAsync(entity, cancellationToken);

        return entity;
    }

    public Task<IQueryable<TEntity>> GetAsync(CancellationToken cancellationToken = default)
    {
        var queryable = _dataSource.GetAsync(cancellationToken);

        return queryable;
    }

    public async Task<TEntity> PatchAsync(TKey id, IDelta<TEntity> delta, CancellationToken cancellationToken = default)
    {
        var queryable = await _dataSource.GetAsync();

        var entityToPatch = await queryable.FindById(id).FirstAsync();

        var entity = await _dataSource.PatchAsync(delta, entityToPatch, cancellationToken);

        return entity;
    }
}
