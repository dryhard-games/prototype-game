namespace Decoy.Core.SoundSystem {
    /// <summary>
    /// SoundEventTypes.cs
    /// <summary>
    /// Author: Thomas van Opstal
    /// </summary>
    public class SoundEventTypes {
        private const string PREFIX = "SOUND_";

        /// <summary>
        /// Lowers/Raises volume based on percentage - Arg: (float)percentage
        /// </summary>
        public const string DAMPING_PUZZLE_MUSIC = PREFIX + "DAMPING_PUZZLE_MUSIC";

        /// <summary>
        /// Plays a single soundtrack - Arg: 1.(SOUNDTRACK_TYPE)Track 2.(float)fadeSpeed 3.(bool)shouldLoop 4.(bool)forceRestart
        /// </summary>
        public const string PLAY_SOUNDTRACK = PREFIX + "PLAY_SOUNDTRACK";
    }
}
