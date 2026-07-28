namespace BGP.Shared.Modules.GameLogic.Kroko.Interfaces;

public interface IKrokoGameDataService
{
    Task<IQueryable<string>> GetRandomWordsAsync(CancellationToken cancellationToken = default);
}
