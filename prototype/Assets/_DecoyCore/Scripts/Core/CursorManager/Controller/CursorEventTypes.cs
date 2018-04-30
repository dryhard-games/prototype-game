namespace Decoy.Core.CursorManager {
    /// <summary>
    /// CursorModel.cs
    /// <summary>
    /// Author: Thomas van Opstal
    /// </summary>
    public class CursorEventTypes {
        private const string PREFIX = "CURSOR_MANAGER_";

        /// <summary>
        /// Arguments: 1.(CursorLockMode)lockMode - 2.(bool)isVisible
        /// </summary>
        public const string CHANGE_CURSOR_SETTINGS = PREFIX + "CHANGE_CURSOR_SETTINGS";

        /// <summary>
        /// Returns the settings to the default of the scene
        /// </summary>
        public const string RETURN_TO_DEFAULT_SETTINGS = PREFIX + "RETURN_TO_DEFAULT_SETTINGS";
    }
}
