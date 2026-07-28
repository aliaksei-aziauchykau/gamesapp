using BGP.Shared.Libs.Domain.Entities;
using BGP.Shared.Libs.Mappings;
using BGP.Shared.Libs.Mappings.Mappings;
using BGP.Shared.Libs.Microservice;
using BGP.Shared.Modules.AutoMapper.Commands;
using BGP.Shared.Modules.CrudCommands.Commands;
using BGP.Shared.Modules.OData.Crud.Api.Helpers.DeltaAdapter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using static Microsoft.AspNetCore.Http.StatusCodes;
using static Microsoft.AspNetCore.OData.Query.AllowedQueryOptions;

namespace BGP.Shared.Modules.OData.Crud.Api;

/// <summary>
/// Base controller for CRUD operations
/// </summary>
/// <typeparam name="TEntity"></typeparam>
/// <typeparam name="TKey"></typeparam>
public abstract class CrudODataControllerExtendedBase<
    TEntity,
    TKey,
    TEntityCreateDto,
    TEntityUpdateDto,
    TEntityPatchDto> : ODataControllerBase
    where TEntity : class, IHasKey<TKey>
    where TEntityCreateDto : IMapTo<TEntity>
    where TEntityUpdateDto : IMapTo<TEntity>
    where TEntityPatchDto : class, IMapToDelta<TEntity>
{
    protected CrudODataControllerExtendedBase(IProvideServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    /// <summary>
    /// Get entity by key
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(Status200OK)]
    [ProducesResponseType(Status400BadRequest)]
    [ProducesResponseType(Status404NotFound)]
    [EnableQuery(AllowedQueryOptions = Select | Expand)]
    public virtual async Task<ActionResult<SingleResult<TEntity?>>> Get([FromRoute] TKey key)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var entity = await Mediator.Send(new GetEntityByIdQueryCommand<TEntity, TKey>(key));

        return SingleResult.Create(entity);
    }

    /// <summary>
    /// Get all entities
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(Status200OK)]
    [ProducesResponseType(Status400BadRequest)]
    [EnableQuery(MaxTop = 100, AllowedQueryOptions = Select | Expand | Filter | Count)]
    public virtual async Task<ActionResult<IQueryable<TEntity>>> Get()
    {
        // ODataQueryOptions<TEntity> query
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var queryable = await Mediator.Send(new GetEntitiesCommand<TEntity, TKey>());

        return Ok(queryable);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="entityCreateDto"></param>
    /// <returns></returns>
    [HttpPost]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType(Status201Created)]
    [ProducesResponseType(Status200OK)]
    [ProducesResponseType(Status400BadRequest)]
    public virtual async Task<ActionResult<TEntity>> Post([FromBody] TEntityCreateDto entityCreateDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var entityToCreate = await Mediator.Send(new MapCommand<TEntityCreateDto, TEntity>(entityCreateDto));

        var entity = await Mediator.Send(new AddEntityCommand<TEntity, TKey>(entityToCreate));

        return Created(entity);
    }

    /// <summary>
    /// Update entity
    /// </summary>
    /// <param name="key"></param>
    /// <param name="entity"></param>
    /// <returns></returns>
    [HttpPut]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType(Status204NoContent)]
    [ProducesResponseType(Status400BadRequest)]
    [ProducesResponseType(Status404NotFound)]
    public async Task<ActionResult<TEntity>> Put([FromRoute] TKey key, [FromBody] TEntityUpdateDto entityUpdateDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var entityToUpdate = await Mediator.Send(new MapCommand<TEntityUpdateDto, TEntity>(entityUpdateDto));

        entityToUpdate.Id = key;

        var entity = await Mediator.Send(new UpdateEntityCommand<TEntity, TKey>(entityToUpdate));

        return Updated(entity);
    }

    /// <summary>
    /// Patch entity
    /// </summary>
    /// <param name="key"></param>
    /// <param name="entity"></param>
    /// <returns></returns>
    [HttpPatch]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType(Status204NoContent)]
    [ProducesResponseType(Status400BadRequest)]
    [ProducesResponseType(Status404NotFound)]
    public async Task<ActionResult<TEntity>> Patch([FromRoute] TKey key, [FromBody] Delta<TEntityPatchDto> entityToPatchDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var delta = DeltaMapper.MapDelta<TEntityPatchDto, TEntity>(entityToPatchDto);

        var entity = await Mediator.Send(new PatchEntityCommand<TEntity, TKey>(key, delta.ToIDelta()));

        return Updated(entity);
    }

    /// <summary>
    /// Delete entity
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    [HttpDelete]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType(Status200OK)]
    [ProducesResponseType(Status400BadRequest)]
    [ProducesResponseType(Status404NotFound)]
    public async Task<ActionResult<TEntity>> Delete([FromRoute] TKey key)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var entity = await Mediator.Send(new DeleteEntityCommand<TEntity, TKey>(key));

        return Ok(entity);
    }
}
