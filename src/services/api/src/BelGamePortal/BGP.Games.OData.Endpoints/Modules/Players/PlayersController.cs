using BGP.Games.Domain.Entities;
using BGP.Games.Microservice.Interfaces;
using BGP.Shared.Modules.OData.Crud.Api;

namespace BGP.Games.OData.Endpoints.Modules.Players;

public class PlayersController(IGamesMicroservice microservice) : CrudODataControllerExtendedBase<Player, Guid, PlayerCreateDto, PlayerUpdateDto, PlayerPatchDto>(microservice)
{
}
