namespace Decoy.Feature.RewiredInput {
    using System.Collections.Generic;
    using Rewired;
    using Decoy.Core.InputSystem;
    using Decoy.Core.GameDataSystem;
    using Decoy.Core.SaveSystem;
    /// <summary>
    /// RewiredInputController.cs
    /// <summary>
    /// Author: Thomas van Opstal
    /// </summary>
    public class RewiredInputController : AbstractInputController {
        public InputModel model;
        public GameDataModel defaultSettingsModel;

        private Player rewiredPlayer;
        private const int masterPlayerId = 0;
        private Controller lastActiveDevice;

        //Prefixes for save related stuff
        private const string savePrefix = "InputMap ";
        private const string keyboardPrefix = "KeyboardMap";
        private const string mousePrefix = "MouseMap";
        private const string gamepadPrefix = "JoystickMap";

        private void Awake() {
            rewiredPlayer = ReInput.players.GetPlayer(masterPlayerId);
        }

        private void Update() {
            model.sprintButtonDown = rewiredPlayer.GetButtonDown(ACTIONS.Sprint.ToString());
            model.sprintButtonUp = rewiredPlayer.GetButtonUp(ACTIONS.Sprint.ToString());
            model.sprintButtonHold = rewiredPlayer.GetButton(ACTIONS.Sprint.ToString());

            model.crouchButtonDown = rewiredPlayer.GetButtonDown(ACTIONS.Crouch.ToString());
            model.crouchButtonUp = rewiredPlayer.GetButtonUp(ACTIONS.Crouch.ToString());
            model.crouchButtonHold = rewiredPlayer.GetButton(ACTIONS.Crouch.ToString());

            model.jumpButtonDown = rewiredPlayer.GetButtonDown(ACTIONS.Jump.ToString());
            model.jumpButtonUp = rewiredPlayer.GetButtonUp(ACTIONS.Jump.ToString());
            model.jumpButtonHold = rewiredPlayer.GetButton(ACTIONS.Jump.ToString());

            model.verticalLookAxis = rewiredPlayer.GetAxis(ACTIONS.VerticalLook.ToString());
            model.verticalLookAxisRaw = rewiredPlayer.GetAxisRaw(ACTIONS.VerticalLook.ToString());

            model.horizontalLookAxis = rewiredPlayer.GetAxis(ACTIONS.HorizontalLook.ToString());
            model.horizontalLookAxisRaw = rewiredPlayer.GetAxisRaw(ACTIONS.HorizontalLook.ToString());

            model.verticalMovementAxis = rewiredPlayer.GetAxis(ACTIONS.VerticalMovement.ToString());
            model.verticalMovementAxisRaw = rewiredPlayer.GetAxisRaw(ACTIONS.VerticalMovement.ToString());

            model.horizontalMovementAxis = rewiredPlayer.GetAxis(ACTIONS.HorizontalMovement.ToString());
            model.horizontalMovementAxisRaw = rewiredPlayer.GetAxisRaw(ACTIONS.HorizontalMovement.ToString());

            model.anyButtonDown = rewiredPlayer.GetAnyButton();
        }

        private void FixedUpdate() {
            if (lastActiveDevice == rewiredPlayer.controllers.GetLastActiveController())
                return;

            lastActiveDevice = rewiredPlayer.controllers.GetLastActiveController();

            CheckLastActiveDevice();
        }

        private void CheckLastActiveDevice() {
            if (lastActiveDevice == null)
                return;

            model.gamepadActive = (lastActiveDevice.type == ControllerType.Joystick);
        }

        public override void ChangeMouseSensitivity(float value) {
            InputBehavior inputBehavior = ReInput.mapping.GetInputBehavior(0, 0);
            inputBehavior.mouseXYAxisSensitivity = value;
        }

        public override void ChangeGamepadSensitivity(float value) {
            InputBehavior inputBehavior = ReInput.mapping.GetInputBehavior(0, 0);
            inputBehavior.joystickAxisSensitivity = value;
        }

        public override void ChangeMouseY(bool value) {
            ToggleInverted(ControllerType.Mouse, value);
        }

        public override void ChangeGamepadY(bool value) {
            ToggleInverted(ControllerType.Joystick, value);
        }

        private void ToggleInverted(ControllerType inputType, bool value) {
            ControllerMap map = rewiredPlayer.controllers.maps.GetMap((int)inputType);

            if (map == null)
                return;

            ActionElementMap[] action = map.GetElementMaps();

            foreach (ActionElementMap actionMap in action) {
                if (actionMap.actionId == (int)ACTIONS.VerticalLook)
                    actionMap.invert = value;
            }
        }

        public override void RumbleGamepadRequest(float duration, float intensity) {
            foreach (Joystick gamepad in rewiredPlayer.controllers.Joysticks) {
                if (!gamepad.supportsVibration)
                    continue;

                for (int i = 0; i < gamepad.vibrationMotorCount; i++)
                    gamepad.SetVibration(i, intensity, duration);
            }
        }

        public override void StopRumbleGamepad() {
            foreach (Joystick gamepad in rewiredPlayer.controllers.Joysticks) {
                gamepad.StopVibration();
            }
        }

        public override void LoadDefaultInputSettings() {
            ToggleInverted(ControllerType.Mouse, defaultSettingsModel.mouseYInverted);
            ToggleInverted(ControllerType.Joystick, defaultSettingsModel.controllerYInverted);

            InputBehavior inputBehavior = ReInput.mapping.GetInputBehavior(0, 0);
            inputBehavior.mouseXYAxisSensitivity = defaultSettingsModel.mouseSensitivity;
            inputBehavior.joystickAxisSensitivity = defaultSettingsModel.controllerSensitivity;
        }

        public override void LoadInputSettings() {
            //Load from the GameDataController directly to make sure the correct model is used.
            ToggleInverted(ControllerType.Mouse, GameDataController.Instance.currentSettingsModel.mouseYInverted);
            ToggleInverted(ControllerType.Joystick, GameDataController.Instance.currentSettingsModel.controllerYInverted);

            InputBehavior inputBehavior = ReInput.mapping.GetInputBehavior(0, 0);
            inputBehavior.mouseXYAxisSensitivity = GameDataController.Instance.currentSettingsModel.mouseSensitivity;
            inputBehavior.joystickAxisSensitivity = GameDataController.Instance.currentSettingsModel.controllerSensitivity;
        }

        public override void SaveInputMap() {
            if (!ReInput.isReady)
                return;

            IList<Player> allPlayers = ReInput.players.AllPlayers;

            for (int i = 0; i < allPlayers.Count; i++) {
                Player player = allPlayers[i];

                PlayerSaveData playerData = player.GetSaveData(true);

                foreach (ControllerMapSaveData saveData in playerData.AllControllerMapSaveData) {
                    if (saveData.controllerType == ControllerType.Joystick) {
                        SaveController.Instance.SaveCustomConfigFile(savePrefix + saveData.map.controllerType.ToString() + " " + saveData.controller.id, saveData.map.ToXmlString());
                        continue;
                    }

                    SaveController.Instance.SaveCustomConfigFile(savePrefix + saveData.map.controllerType.ToString(), saveData.map.ToXmlString());
                }
            }
        }

        public override void LoadInputMap() {
            if (!ReInput.isReady)
                return;

            string[] inputMaps = SaveController.Instance.GetMultipleCustomConfigFile(savePrefix);

            if (inputMaps == null)
                return;

            List<string> keyboardMaps = new List<string>();
            List<string> mouseMaps = new List<string>();
            List<string> gamepadMaps = new List<string>();

            foreach (string map in inputMaps) {
                if (map.Contains(keyboardPrefix))
                    keyboardMaps.Add(map);
                else if (map.Contains(mousePrefix))
                    mouseMaps.Add(map);
                else if (map.Contains(gamepadPrefix))
                    gamepadMaps.Add(map);
            }

            rewiredPlayer.controllers.maps.AddMapsFromXml(ControllerType.Keyboard, 0, keyboardMaps);
            rewiredPlayer.controllers.maps.AddMapsFromXml(ControllerType.Mouse, 0, mouseMaps);

            if (rewiredPlayer.controllers.Joysticks.Count == 0)
                return;

            foreach (Joystick joystick in rewiredPlayer.controllers.Joysticks) {
                foreach (string map in gamepadMaps) {
                    if (map.Contains(joystick.hardwareTypeGuid.ToString()))
                        rewiredPlayer.controllers.maps.AddMapFromXml(ControllerType.Joystick, joystick.id, map);
                }
            }
        }
    }
}
