using System.Net.Http.Json;
using BGP.Shared.Libs.Domain.Entities;
using BGP.Shared.Libs.Mappings.Mappings;
using BGP.Shared.Modules.EntityApi.Exceptions;
using BGP.Shared.Modules.EntityApi.Extensions;
using BGP.Shared.Modules.EntityApi.OData;

namespace BGP.Shared.Modules.EntityApi;

public class EntityApiClient<TEntity, TKey> : IEntityApiClient<TEntity, TKey>
    where TEntity : class, IHasKey<TKey>
{
    private readonly HttpClient _httpClient;
    private readonly string apiUrl;

    public EntityApiClient(HttpClient httpClient, IEntityApiClientConfig config)
    {
        var entityName = typeof(TEntity).Name;
        var collectionName = entityName.ToEntityCollections(config.EntityDtoType);

        apiUrl = $"{config.BaseUrl}/{collectionName}";
        _httpClient = httpClient;
    }

    public async Task<IList<TEntity>> GetAllAsync(ODataQuery oDataQuery, CancellationToken cancellationToken = default)
    {
        var url = $"{apiUrl}/{oDataQuery.Query}";

        var entities = await _httpClient.GetFromJsonAsync<ODataResponse<IList<TEntity>>>(url, cancellationToken);

        if (entities == null) throw new NotFoundException($"Entities are not found");

        return entities.Value;
    }

    public async Task<IList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var entities = await _httpClient.GetFromJsonAsync<ODataResponse<IList<TEntity>>>(apiUrl, cancellationToken);

        if (entities == null) throw new NotFoundException($"Entities are not found");

        return entities.Value;
    }

    public async Task<TEntity> GetByIdAsync(TKey id, ODataQuery oDataQuery, CancellationToken cancellationToken = default)
    {
        var url = $"{apiUrl}/{oDataQuery.Query}";

        var entity = await _httpClient.GetFromJsonAsync<TEntity>(url, cancellationToken);

        if (entity == null) throw new NotFoundException($"Entity with id: {id} is not found");

        return entity;
    }

    public async Task<TEntity> GetByIdAsync(TKey id, CancellationToken cancellationToken = default)
    {
        var entity = await _httpClient.GetFromJsonAsync<TEntity>($"{apiUrl}/{id}", cancellationToken);

        if (entity == null) throw new NotFoundException($"Entity with id: {id} is not found");

        return entity;
    }

    public async Task<TEntity> AddAsync<TEntityCreateDto>(TEntityCreateDto entity, CancellationToken cancellationToken = default) where TEntityCreateDto : IMapTo<TEntity>
    {
        var response = await _httpClient.PostAsJsonAsync(apiUrl, entity, cancellationToken);
        response.EnsureSuccessStatusCode();

        var entityToReturn = await response.Content.ReadFromJsonAsync<TEntity>();

        return entityToReturn;
    }

    public async Task<TEntity> UpdateAsync<TEntityUpdateDto>(TEntityUpdateDto entity, CancellationToken cancellationToken = default) where TEntityUpdateDto : IMapTo<TEntity>, IHasKey<TKey>
    {
        var response = await _httpClient.PutAsJsonAsync($"{apiUrl}/{entity.Id}", entity, cancellationToken);
        response.EnsureSuccessStatusCode();

        var entityToReturn = await response.Content.ReadFromJsonAsync<TEntity>();

        return entityToReturn;
    }

    public async Task<TEntity> PatchAsync<TEntityPatchDto>(TEntityPatchDto entity, CancellationToken cancellationToken = default) where TEntityPatchDto : IMapTo<TEntity>, IHasKey<TKey>
    {
        var response = await _httpClient.PatchAsJsonAsync($"{apiUrl}/{entity.Id}", entity, cancellationToken);
        response.EnsureSuccessStatusCode();

        var entityToReturn = await response.Content.ReadFromJsonAsync<TEntity>();

        return entityToReturn;
    }

    public async Task<TEntity> DeleteAsync(TKey id, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.DeleteAsync($"{apiUrl}/{id}", cancellationToken);
        response.EnsureSuccessStatusCode();

        var entityToReturn = await response.Content.ReadFromJsonAsync<TEntity>();

        return entityToReturn;
    }
}
