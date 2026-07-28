


using BGP.Games.Domain.Entities.Games.Kroko;
using BGP.Games.Microservice.Interfaces;
using BGP.Shared.Modules.OData.Crud.Api;

namespace BGP.Games.OData.Endpoints.Modules.Kroko.WordLanguage;

public class KrokoWordLanguagesController(IGamesMicroservice microservice) : CrudODataControllerBase<KrokoWordLanguage, int>
(microservice)
{
}
