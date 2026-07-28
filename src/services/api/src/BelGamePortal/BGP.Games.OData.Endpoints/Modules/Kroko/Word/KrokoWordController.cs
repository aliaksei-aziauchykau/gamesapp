


using BGP.Games.Domain.Entities.Games.Kroko;
using BGP.Games.Microservice.Interfaces;
using BGP.Games.OData.Endpoints.Modules.Kroko.Word.Dtos;
using BGP.Shared.Modules.OData.Crud.Api;

namespace BGP.Games.OData.Endpoints.Modules.Kroko.Word;

public class KrokoWordsController(IGamesMicroservice microservice) : CrudODataControllerExtendedBase<KrokoWord, int, KrokoWordCreateDto, KrokoWordUpdateDto, KrokoWordPatchDto>
(microservice)
{
}
