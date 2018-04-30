namespace Decoy.Core.GameDataSystem {
    /// <summary>
    /// GameDataEventTypes.cs
    /// <summary>
    /// Author: Thomas van Opstal
    /// </summary>
    public class GameDataEventTypes {
        private const string PREFIX = "GAME_DATA_";

        public const string LOAD_DEFAULT_SETTINGS = PREFIX + "LOAD_DEFAULT_SETTINGS";
        public const string LOAD_DEFAULT_AUDIO = PREFIX + "LOAD_DEFAULT_AUDIO";
        public const string LOAD_DEFAULT_GRAPHICS = PREFIX + "LOAD_DEFAULT_GRAPHICS";
        public const string LOAD_DEFAULT_GAMEPLAY = PREFIX + "LOAD_DEFAULT_GAMEPLAY";
        public const string LOAD_DATA_FROM_SAVE = PREFIX + "LOAD_DATA_FROM_SAVE";

        public const string GAME_DATA_READY = PREFIX + "GAME_DATA_READY";
    }
}
