export namespace KrokoGameSocketCommands {
    export class ToClient {
        public static GameContext = 'GameContext';
        public static GameSettings = 'GameSettings';
        public static PlayerGameContext = 'PlayerGameContext';
        public static SelectWord = 'SelectWordCommand';
        public static CloseAllModals = 'CloseAllModalsCommand';
        public static ReloadShapes = 'ReloadShapesCommand';
    }

    export class ToServer {
        public static JoinPlayer = 'JoinPlayerCommand';
        public static LeavePlayer = 'LeavePlayerCommand';
        public static GuessWord = 'GuessWordCommand';
        public static DrawWord = 'DrawWordCommand';
        public static FetchGameContext = 'GetGameContextCommand';
        public static FetchGameSettings = 'GetGameSettingsCommand';
        public static StoreSelectedWord = 'StoreSelectedWordCommand';
        public static SaveGameSettings = 'SaveGameSettingsCommand';
        public static StartGame = 'StartGameCommand';
    }
}
