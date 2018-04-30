namespace Decoy.Core.InputSystem {
    using System;
    using UnityEngine;
    /// <summary>
    /// AbstractInputController.cs
    /// <summary>
    /// Author: Thomas van Opstal
    /// </summary>
    [Serializable]
    public abstract class AbstractInputController : MonoBehaviour {
        /// <summary>
        /// Change the mouse sensitivity to the requested value
        /// </summary>
        /// <param name="value">new sensitivity</param>
        public abstract void ChangeMouseSensitivity(float value);

        /// <summary>
        /// Change the controller sensitivity to the requested value
        /// </summary>
        /// <param name="value">new sensitivity</param>
        public abstract void ChangeGamepadSensitivity(float value);

        /// <summary>
        /// Invert the mouse Y Axis
        /// </summary>
        /// <param name="value">new invert state</param>
        public abstract void ChangeMouseY(bool value);

        /// <summary>
        /// Invert the controller Y Axis
        /// </summary>
        /// <param name="value">new invert state</param>
        public abstract void ChangeGamepadY(bool value);

        /// <summary>
        /// Gamepad rumble request
        /// </summary>
        /// <param name="value">1.duration - 2.intensity(0f-1f)  of the rumble</param>
        public abstract void RumbleGamepadRequest(float duration, float intensity);

        /// <summary>
        /// Gamepad rumble stop request
        /// </summary>
        public abstract void StopRumbleGamepad();

        /// <summary>
        /// Load default settings from the default input settings model
        /// </summary>
        public abstract void LoadDefaultInputSettings();

        /// <summary>
        /// Load settings from the current settings model
        /// </summary>
        public abstract void LoadInputSettings();

        /// <summary>
        /// Save new input map of the player
        /// </summary>
        public abstract void SaveInputMap();

        /// <summary>
        /// Load input map of the player
        /// </summary>
        public abstract void LoadInputMap();
    }
}
