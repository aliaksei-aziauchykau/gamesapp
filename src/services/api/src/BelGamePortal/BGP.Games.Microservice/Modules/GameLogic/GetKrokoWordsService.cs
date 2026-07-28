using BGP.Shared.Modules.CrudCommands.Repositories;

using KrokoWordDomain = BGP.Games.Domain.Entities.Games.Kroko.KrokoWord;
using BGP.Shared.Modules.GameLogic.Kroko.Interfaces;

internal class KrokoGameDataService(IRepository<KrokoWordDomain, int> _repository) : IKrokoGameDataService
{
    public async Task<IQueryable<string>> GetRandomWordsAsync(CancellationToken cancellationToken = default)
    {
        var queryable = await _repository.GetAsync(cancellationToken);

        var words = queryable.Select(x => x.Word);

        return words;
    }
}
