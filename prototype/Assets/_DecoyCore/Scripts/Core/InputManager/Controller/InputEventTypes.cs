namespace Decoy.Core.InputSystem {
    /// <summary>
    /// InputEventTypes.cs
    /// <summary>
    /// Author: Thomas van Opstal
    /// </summary>
    public class InputEventTypes {
        private const string PREFIX = "INPUT_";

        /// <summary>
        /// Loads the settings that are loaded in from the Save File. (using CurrentSettingsModel)
        /// </summary>
        public const string LOAD_INPUT_SETTINGS = PREFIX + "LOAD_INPUT_SETTINGS";

        /// <summary>
        /// Sets - 1.MouseYInverted - 2.ControllerYInverted - 3.MouseSensitivity - 4.ControllerSensitivity
        /// </summary>
        public const string LOAD_DEFAULT_INPUT_SETTINGS = PREFIX + "LOAD_DEFAULT_INPUT_SETTINGS";

        /// <summary>
        /// Save the input map of the player
        /// </summary>
        public const string SAVE_INPUT_MAP = PREFIX + "SAVE_INPUT_MAP";

        /// <summary>
        /// Load the saved input maps of the player
        /// </summary>
        public const string LOAD_INPUT_MAP = PREFIX + "LOAD_INPUT_MAP";

        /// <summary>
        /// Arguments - 1.(bool)isGamepadActive
        /// </summary>
        public const string GAMEPAD_STATUS_CHANGED = PREFIX + "GAMEPAD_STATUS_CHANGED";

        /// <summary>
        /// Arguments - 1.(float)newValue
        /// </summary>
        public const string ON_MOUSE_SENSITIVITY_CHANGED = PREFIX + "ON_MOUSE_SENSITIVITY_CHANGED";

        /// <summary>
        /// Arguments - 1.(float)newValue
        /// </summary>
        public const string ON_GAMEPAD_SENSITIVITY_CHANGED = PREFIX + "ON_GAMEPAD_SENSITIVITY_CHANGED";

        /// <summary>
        /// Arguments - 1.(bool)newState
        /// </summary>
        public const string ON_MOUSE_Y_CHANGED = PREFIX + "ON_MOUSE_Y_CHANGED";

        /// <summary>
        /// Arguments - 1.(bool)newState
        /// </summary>
        public const string ON_GAMEPAD_Y_CHANGED = PREFIX + "ON_GAMEPAD_Y_CHANGED";

        /// <summary>
        /// Arguments - 1.(float)duration - 2.(float)intensity
        /// </summary>
        public const string RUMBLE_GAMEPAD_REQUEST = PREFIX + "RUMBLE_GAMEPAD_REQUEST";
        public const string STOP_RUMBLE_GAMEPAD = PREFIX + "STOP_RUMBLE_GAMEPAD";
    }
}
