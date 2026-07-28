using BGP.Games.Domain.Entities;
using BGP.Games.Microservice.Interfaces;
using BGP.Shared.Modules.OData.Crud.Api;

namespace BGP.Games.OData.Endpoints.Modules.Games;

public class GamesController(IGamesMicroservice microservice) : CrudODataControllerExtendedBase<Game, Guid, GameCreateDto, GameUpdateDto, GamePatchDto>(microservice)
{
}
