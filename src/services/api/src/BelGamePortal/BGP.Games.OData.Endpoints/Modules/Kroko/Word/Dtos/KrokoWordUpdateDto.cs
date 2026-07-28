using BGP.Games.Domain.Entities.Games.Kroko;
using BGP.Shared.Libs.Domain.Entities;
using BGP.Shared.Libs.Mappings.Mappings;

namespace BGP.Games.OData.Endpoints.Modules.Kroko.Word.Dtos;

public class KrokoWordUpdateDto : IMapTo<KrokoWord>
{
    public int Id { get; set; }

    public string Word { get; set; }

    public string Meaning { get; set; }

    public int LanguageId { get; set; }
}
