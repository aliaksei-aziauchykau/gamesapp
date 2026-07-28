using BGP.Games.Domain.Entities;
using BGP.Games.Microservice.Interfaces;
using BGP.Shared.Libs.FormEntitySchemas.Models.EntityFormSchema;
using BGP.Shared.Libs.FormEntitySchemas.Models.EntityFormSchema.Controls;
using ITD.Core.Application.Commands.FormSchema;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Attributes;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OData;
using static Microsoft.AspNetCore.Http.StatusCodes;
using static Microsoft.AspNetCore.OData.Query.AllowedQueryOptions;


namespace BGP.Games.OData.Endpoints.Modules.Games;

public class GamesUiSchemasController(IGamesMicroservice microservice) : ODataController
{
    public IMediator Mediator => microservice.ServiceProvider.CreateScope().ServiceProvider.GetRequiredService<IMediator>();

    [HttpGet("api/v{version:apiVersion}/GamesUiSchemas/GetUiSchema")]
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
            "Games" => new GetEntityFormSchemaCommand<Game>(),
            "GameDefinitions" => new GetEntityFormSchemaCommand<GameDefinition>(),
            "Posts" => new GetEntityFormSchemaCommand<Post>(),
            _ => throw new InvalidOperationException()
        };

        var result = await Mediator.Send(command);

        return Ok(result);
    }
}
