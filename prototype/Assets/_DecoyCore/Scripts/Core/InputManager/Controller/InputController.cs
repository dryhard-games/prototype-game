namespace Decoy.Core.InputSystem {
    using UnityEngine;
    using Decoy.Core.EventSystem;
    /// <summary>
    /// InputController.cs
    /// <summary>
    /// Author: Thomas van Opstal
    /// </summary>
    public class InputController : MonoBehaviour {
        public AbstractInputController inputInterface;

        private void Start() {
            EventManager.StartListening(InputEventTypes.ON_MOUSE_SENSITIVITY_CHANGED, OnMouseSensitivityChanged);
            EventManager.StartListening(InputEventTypes.ON_GAMEPAD_SENSITIVITY_CHANGED, OnGamepadSensitivityChanged);
            EventManager.StartListening(InputEventTypes.ON_MOUSE_Y_CHANGED, OnMouseYChanged);
            EventManager.StartListening(InputEventTypes.ON_GAMEPAD_Y_CHANGED, OnGamepadYChanged);
            EventManager.StartListening(InputEventTypes.RUMBLE_GAMEPAD_REQUEST, OnRumbleGamepadRequest);
            EventManager.StartListening(InputEventTypes.STOP_RUMBLE_GAMEPAD, OnStopRumbleRequest);
            EventManager.StartListening(InputEventTypes.LOAD_DEFAULT_INPUT_SETTINGS, OnLoadDefaultInputSettingsRequest);
            EventManager.StartListening(InputEventTypes.LOAD_INPUT_SETTINGS, OnLoadInputSettingsRequest);
            EventManager.StartListening(InputEventTypes.SAVE_INPUT_MAP, OnSaveInputMapRequest);
            EventManager.StartListening(InputEventTypes.LOAD_INPUT_MAP, OnLoadInputMapRequest);
        }

        private void OnDestroy() {
            EventManager.StopListening(InputEventTypes.ON_MOUSE_SENSITIVITY_CHANGED, OnMouseSensitivityChanged);
            EventManager.StopListening(InputEventTypes.ON_GAMEPAD_SENSITIVITY_CHANGED, OnGamepadSensitivityChanged);
            EventManager.StopListening(InputEventTypes.ON_MOUSE_Y_CHANGED, OnMouseYChanged);
            EventManager.StopListening(InputEventTypes.ON_GAMEPAD_Y_CHANGED, OnGamepadYChanged);
            EventManager.StopListening(InputEventTypes.RUMBLE_GAMEPAD_REQUEST, OnRumbleGamepadRequest);
            EventManager.StopListening(InputEventTypes.STOP_RUMBLE_GAMEPAD, OnStopRumbleRequest);
            EventManager.StopListening(InputEventTypes.LOAD_DEFAULT_INPUT_SETTINGS, OnLoadDefaultInputSettingsRequest);
            EventManager.StopListening(InputEventTypes.LOAD_INPUT_SETTINGS, OnLoadInputSettingsRequest);
            EventManager.StopListening(InputEventTypes.SAVE_INPUT_MAP, OnSaveInputMapRequest);
            EventManager.StopListening(InputEventTypes.LOAD_INPUT_MAP, OnLoadInputMapRequest);
        }

        private void OnGamepadYChanged(object[] arg0) {
            inputInterface.ChangeGamepadY((bool)arg0[0]);
        }

        private void OnMouseYChanged(object[] arg0) {
            inputInterface.ChangeMouseY((bool)arg0[0]);
        }

        private void OnGamepadSensitivityChanged(object[] arg0) {
            inputInterface.ChangeGamepadSensitivity((float)arg0[0]);
        }

        private void OnMouseSensitivityChanged(object[] arg0) {
            inputInterface.ChangeMouseSensitivity((float)arg0[0]);
        }

        private void OnRumbleGamepadRequest(object[] arg0) {
            inputInterface.RumbleGamepadRequest((float)arg0[0], (float)arg0[1]);
        }

        private void OnStopRumbleRequest(object[] arg0) {
            inputInterface.StopRumbleGamepad();
        }

        private void OnLoadDefaultInputSettingsRequest(object[] arg0) {
            inputInterface.LoadDefaultInputSettings();
        }

        private void OnLoadInputSettingsRequest(object[] arg0) {
            inputInterface.LoadInputSettings();
        }

        private void OnLoadInputMapRequest(object[] arg0) {
            inputInterface.LoadInputMap();
        }

        private void OnSaveInputMapRequest(object[] arg0) {
            inputInterface.SaveInputMap();
        }
    }
}
