using BGP.Auth.Domain.Entities;
using BGP.Auth.Microservice.Interfaces;
using BGP.Shared.Modules.CrudCommands.Commands;
using BGP.Shared.Modules.OData.Crud.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using static Microsoft.AspNetCore.Http.StatusCodes;
using static Microsoft.AspNetCore.OData.Query.AllowedQueryOptions;

namespace BGP.Auth.OData.Endpoints.Modules.Users;

public class AuthUsersController(IAuthMicroservice serviceProvider) : ODataControllerBase(serviceProvider)
{
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(Status200OK)]
    [ProducesResponseType(Status400BadRequest)]
    [ProducesResponseType(Status404NotFound)]
    [EnableQuery(AllowedQueryOptions = Select | Expand)]
    public virtual async Task<ActionResult<AuthUser?>> Get([FromRoute] Guid key)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var entity = await Mediator.Send(new GetEntityByIdCommand<AuthUser, Guid>(key));

        return Ok(entity);
    }


    [HttpGet]
    [ProducesResponseType(Status200OK)]
    [ProducesResponseType(Status400BadRequest)]
    [EnableQuery(MaxTop = 100, AllowedQueryOptions = Select | Expand | Filter | Count)]
    public virtual async Task<ActionResult<IList<AuthUser>>> Get()
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var queryable = await Mediator.Send(new GetAllEntitiesCommand<AuthUser, Guid>());

        return Ok(queryable);
    }
}
