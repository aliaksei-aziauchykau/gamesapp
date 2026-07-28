import { GameDefNames } from '@itdl/game-portal/features/game-definition/entities';
import { IDynamicLoaderConfiguration } from '@itdl/shared/dynamic-loader';

export const lobbyContentLazyModulesConfig: IDynamicLoaderConfiguration = {
    devMode: true,
    moduleConfigs: {
        [GameDefNames.Kroko]: {
            loadModule: () =>
                import(
                    /* webpackChunkName: "gpll-kroko-lobby.module.chunk" */
                    '@itdl/game-portal/features/games/kroko-game/lobby'
                ).then((m) => m.GpllKrokoLobbyModule),
        },
    },
};
