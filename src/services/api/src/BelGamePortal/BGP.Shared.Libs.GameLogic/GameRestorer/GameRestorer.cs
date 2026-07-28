//using Unity;

//namespace BGP.Shared.Libs.GameLogic;

//public class GameRestorer<T> : IGameRestorer<T> where T : IGameContext
//{
//    private readonly IUnityContainer _unityContainer;
//    private readonly IGameStateRestorer<T> _gameStateRestorer;
//    private readonly IGameLogic _gameLogic;

//    public GameRestorer(IUnityContainer unityContainer)
//    {
//        _unityContainer = unityContainer;
//    }

//    public void Restore(T gameContext)
//    {
//        var gameState = _gameStateRestorer.Restore(gameContext);
//        // _gameLogic.SetState(gameState);
//        _unityContainer.RegisterInstance(typeof(T), gameContext);
//    }
//}
