const ROOT = '@@State';
export class StorageKeys {
    public static readonly GameContainerState = `${ROOT}.gameContainerState`;
    public static readonly GameDefinitionState = `${ROOT}.gameDefinitionState`;
    public static readonly GameState = `${ROOT}.gameState`;
    public static readonly KrokoGameState = `${ROOT}.krokoGameState`;
    public static readonly User = `${ROOT}.user`;
    public static readonly UserPreferences = `${ROOT}.userPreferences`;

    public static get keys() {
        return Object.keys(StorageKeys);
    }
}
