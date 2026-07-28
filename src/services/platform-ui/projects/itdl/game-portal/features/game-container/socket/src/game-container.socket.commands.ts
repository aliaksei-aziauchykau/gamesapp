export namespace GameContainerSocketCommands {
    export class ToClient {
        public static GameContainerState = 'GameContainerState';
        public static GameContext = 'GameContext';
        public static GameStateChanged = 'GameStateChanged';
    }

    export class ToServer {
        public static Create = 'Create';
        public static Start = 'Start';
        public static Stop = 'Stop';
        public static Initialize = 'Initialize';
        public static Destroy = 'Destroy';
        public static GetState = 'GetState';
        public static GetGameContext = 'GetGameContext';
        public static ConnectCommand = 'ConnectCommand';
        public static DisconnectCommand = 'DisconnectCommand';
    }
}
