using BGP.Games.Domain.Entities;
using BGP.Games.Microservice.Interfaces;
using BGP.Shared.Modules.OData.Crud.Api;

namespace BGP.Games.OData.Endpoints.Modules.GameDefinitions;

public class GameDefinitionsController(IGamesMicroservice microservice) : CrudODataControllerExtendedBase<GameDefinition, int, GameDefinitionCreateDto, GameDefinitionUpdateDto, GameDefinitionPatchDto>(microservice)
{
}
