import { GameDefNames } from '@itdl/game-portal/features/game-definition/entities';
import { IDynamicLoaderConfiguration } from '@itdl/shared/dynamic-loader';

export const gameContentLazyModulesConfig: IDynamicLoaderConfiguration = {
    devMode: true,
    moduleConfigs: {
        [GameDefNames.Kroko]: {
            loadModule: () =>
                import(
                    /* webpackChunkName: "gpll-kroko-game.module.chunk" */
                    '@itdl/game-portal/features/games/kroko-game'
                ).then((m) => m.GpllKrokoGameModule),
        },
        [GameDefNames.GeoGuesser]: {
            loadModule: () =>
                import(
                    /* webpackChunkName: "gpll-geo-guesser-game.module.chunk" */
                    '@itdl/game-portal/features/games/geo-guesser-game'
                ).then((m) => m.GpllGeoGuesserGameModule),
        },
        [GameDefNames.PhaserTest]: {
            loadModule: () =>
                import(
                    /* webpackChunkName: "gpll-geo-guesser-game.module.chunk" */
                    '@itdl/game-portal/features/games/phaser-test-game'
                ).then((m) => m.GpllPhaserTestGameModule),
        },
    },
};
