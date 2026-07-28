
using BGP.Platform.Domain.Entities;
using BGP.Platform.Microservice.Interfaces;
using BGP.Shared.Libs.FormEntitySchemas.Models.EntityFormSchema;
using ITD.Core.Application.Commands.FormSchema;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OData;
using static Microsoft.AspNetCore.Http.StatusCodes;


namespace BGP.Games.OData.Endpoints.Modules.Games;

public class PlatformUiSchemasController(IPlatformMicroservice microservice) : ODataController
{
    public IMediator Mediator => microservice.ServiceProvider.CreateScope().ServiceProvider.GetRequiredService<IMediator>();

    [HttpGet("api/v{version:apiVersion}/PlatformUiSchemas/GetUiSchema")]
    [Produces("application/json")]
    [ProducesResponseType(Status200OK)]
    [ProducesResponseType(Status400BadRequest)]
    [ProducesResponseType(Status404NotFound)]
    public async Task<ActionResult> GetUiSchema([FromQuery] string collectionSchemaName)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        IRequest<IFormEntitySchema> command = collectionSchemaName switch
        {
            "Posts" => new GetEntityFormSchemaCommand<Post>(),
            _ => throw new InvalidOperationException()
        };

        var result = await Mediator.Send(command);

        return Ok(result);
    }
}
