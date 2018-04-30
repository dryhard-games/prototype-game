namespace Decoy.Core.Player {
    using UnityEngine;
    using Decoy.Core.InputSystem;
    using Decoy.Core.GameDataSystem;
    /// <summary>
    /// PlayerCameraController.cs
    /// <summary>
    /// Author: Thomas van Opstal
    /// <summary>
    /// PlayerCameraControls for a first person camera
    /// </summary>
    public class PlayerCameraController : MonoBehaviour {
        public GameObject characterBody;
        public PlayerModel model;
        public InputModel inputModel;

        private Vector2 targetDir;
        private Vector2 targetCharDir;

        private Vector2 absoluteValue;
        private Vector2 smoothValue;

        private void Start() {
            targetDir = transform.localRotation.eulerAngles;

            if (characterBody)
                targetCharDir = characterBody.transform.localRotation.eulerAngles;
        }

        private void Update() {
            if (!inputModel.detectInput)
                return;

            if (inputModel.gamepadActive)
                DetectGamepadInput();
            else
                DetectMouseInput();

            UpdateRotation();
        }

        /// <summary>
        /// Detect gamepad input
        /// </summary>
        private void DetectGamepadInput() {
            Vector2 controllerDelta = new Vector2(inputModel.horizontalLookAxisRaw, inputModel.verticalLookAxisRaw);

            controllerDelta = Vector2.Scale(controllerDelta, new Vector2(GameDataController.Instance.ControllerSensitivity * model.cameraSmoothing, GameDataController.Instance.ControllerSensitivity * model.cameraSmoothing));

            smoothValue.x = Mathf.Lerp(smoothValue.x, controllerDelta.x, 1f / model.cameraSmoothing);
            smoothValue.y = Mathf.Lerp(smoothValue.y, controllerDelta.y, 1f / model.cameraSmoothing);

            absoluteValue += smoothValue;
        }

        /// <summary>
        /// Detect mouse input
        /// </summary>
        private void DetectMouseInput() {
            if (GameDataController.Instance.MouseSmoothing) {
                Vector2 mouseDelta = new Vector2(inputModel.horizontalLookAxis, inputModel.verticalLookAxis);
                mouseDelta = Vector2.Scale(mouseDelta, new Vector2(GameDataController.Instance.MouseSensitivity * model.cameraSmoothing, GameDataController.Instance.MouseSensitivity * model.cameraSmoothing));

                smoothValue.x = Mathf.Lerp(smoothValue.x, mouseDelta.x, 1f / model.cameraSmoothing);
                smoothValue.y = Mathf.Lerp(smoothValue.y, mouseDelta.y, 1f / model.cameraSmoothing);

                absoluteValue += smoothValue;
            } else {
                Vector2 mouseDelta = new Vector2(inputModel.horizontalLookAxisRaw, inputModel.verticalLookAxisRaw);
                mouseDelta = new Vector2(mouseDelta.x * GameDataController.Instance.MouseSensitivity, mouseDelta.y * GameDataController.Instance.MouseSensitivity);
                absoluteValue += mouseDelta;
            }
        }

        /// <summary>
        /// Update rotation of the camera based on mouse/gamepad input
        /// </summary>
        private void UpdateRotation() {
            Quaternion targetOrientation = Quaternion.Euler(targetDir);
            Quaternion targetCharacterOrientation = Quaternion.Euler(targetCharDir);

            if (model.cameraClamp.x < 360)
                absoluteValue.x = Mathf.Clamp(absoluteValue.x, -model.cameraClamp.x * 0.5f, model.cameraClamp.x * 0.5f);

            if (model.cameraClamp.y < 360)
                absoluteValue.y = Mathf.Clamp(absoluteValue.y, -model.cameraClamp.y * 0.5f, model.cameraClamp.y * 0.5f);

            transform.localRotation = Quaternion.AngleAxis(-absoluteValue.y, targetOrientation * Vector3.right) * targetOrientation;

            if (characterBody) {
                Quaternion yRotation = Quaternion.AngleAxis(absoluteValue.x, Vector3.up);
                characterBody.transform.localRotation = yRotation * targetCharacterOrientation;
            } else {
                Quaternion yRotation = Quaternion.AngleAxis(absoluteValue.x, transform.InverseTransformDirection(Vector3.up));
                transform.localRotation *= yRotation;
            }
        }
    }
}
