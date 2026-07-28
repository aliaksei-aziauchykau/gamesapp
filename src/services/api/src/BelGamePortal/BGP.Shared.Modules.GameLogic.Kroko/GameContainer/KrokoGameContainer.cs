using BGP.Shared.Libs.GameLogic;
using Unity;

namespace BGP.Shared.Modules.GameLogic.Kroko.Container;

public class KrokoGameContainer : GameContainer
{
    public KrokoGameContainer(IUnityContainer container) : base(container)
    {
    }

    public override async Task InitializeAsync()
    {
        var _gameRestorer = this.UnityContainer.Resolve<IGameRestorer>();
        var _gameContext = this.GameContext;

        await _gameRestorer.RestoreAsync(_gameContext);

        await base.InitializeAsync();
    }
}
