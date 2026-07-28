
using static Microsoft.AspNetCore.OData.Query.AllowedQueryOptions;
using static Microsoft.AspNetCore.Http.StatusCodes;
using BGP.Shared.Libs.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.OData.Query;
using BGP.Shared.Modules.CrudCommands.Commands;
using Microsoft.AspNetCore.OData.Deltas;
using BGP.Shared.Libs.Mappings;
using BGP.Shared.Modules.OData.Crud.Api.Helpers.DeltaAdapter;
using BGP.Shared.Libs.Microservice;

namespace BGP.Shared.Modules.OData.Crud.Api;

/// <summary>
/// Base controller for CRUD operations
/// </summary>
/// <typeparam name="TEntity"></typeparam>
/// <typeparam name="TKey"></typeparam>
public abstract class CrudODataControllerBase<TEntity, TKey> : ODataControllerBase
    where TEntity : class, IHasKey<TKey>, new()
{

    protected CrudODataControllerBase(IProvideServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    /// <summary>
    /// Get entity by key
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [EnableQuery(AllowedQueryOptions = Select | Expand)]
    public virtual async Task<ActionResult<TEntity?>> Get([FromRoute] TKey key)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var command = new GetEntityByIdCommand<TEntity, TKey>(key);

        var result = await Mediator.Send(command);

        return Ok(result);
    }

    /// <summary>
    /// Get all entities
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [EnableQuery(MaxTop = 200, AllowedQueryOptions = Select | Expand | Filter)]
    public virtual async Task<ActionResult<IQueryable<TEntity>>> Get()
    {
        // ODataQueryOptions<TEntity> query
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var command = new GetEntitiesCommand<TEntity, TKey>();
        var queryable = await Mediator.Send(command);

        //query.ApplyTo(queryable).ToDynamicList();
        return Ok(queryable);
    }

    /// <summary>
    /// Create entity
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    [HttpPost]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TEntity>> Post([FromBody] TEntity entity)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var command = new AddEntityCommand<TEntity, TKey>(entity);
        return await Mediator.Send(command);
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
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TEntity>> Put([FromRoute] TKey key, [FromBody] TEntity entity)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        entity.Id = key;
        var command = new UpdateEntityCommand<TEntity, TKey>(entity);
        return await Mediator.Send(command);
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
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [EnableQuery(AllowedQueryOptions = Select | Expand)]
    public async Task<ActionResult<TEntity>> Patch([FromRoute] TKey key, [FromBody] Delta<TEntity> entity)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var command = new PatchEntityCommand<TEntity, TKey>(key, entity.ToIDelta());
        return await Mediator.Send(command);
    }

    /// <summary>
    /// Delete entity
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    [HttpDelete]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TEntity>> Delete([FromRoute] TKey key)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var command = new DeleteEntityCommand<TEntity, TKey>(key);
        return await Mediator.Send(command);
    }

    //[HttpGet]
    //[Produces("application/json")]
    //[Consumes("application/json")]
    //[ProducesResponseType(Status200OK)]
    //[ProducesResponseType(Status400BadRequest)]
    //public async Task<IActionResult> FormSchema()
    //{
    //    if (!ModelState.IsValid)
    //    {
    //        return BadRequest(ModelState);
    //    }

    //    var command = new GetEntityFormSchemaCommand<Game>();

    //    var result = await Mediator.Send(command);

    //    return Ok(result);
    //}
}
