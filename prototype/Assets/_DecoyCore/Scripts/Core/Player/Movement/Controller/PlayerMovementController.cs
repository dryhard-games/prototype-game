namespace Decoy.Core.Player {
    using UnityEngine;
    using Decoy.Core.InputSystem;
    using Decoy.Core.GameDataSystem;
    /// <summary>
    /// PlayerMovementController.cs
    /// <summary>
    /// Author: Thomas van Opstal
    /// <summary>
    /// PlayerMovementController
    /// </summary>
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovementController : MonoBehaviour {
        public PlayerModel model;
        public InputModel inputModel;

        private float actualSpeed = 4.0f;
        private float verticalVelocity = 0.0f;
        private Vector3 direction = Vector3.zero;
        private CharacterController characterController;

        private bool isCrouching;
        private bool isSprinting;
        private bool sprintStopping;

        private void Start() {
            characterController = GetComponent<CharacterController>();
        }

        private void OnInputEnabled(object[] arg0) {
            inputModel.detectInput = true;
            actualSpeed = 4.0f;
        }

        private void OnInputBlocked(object[] arg0) {
            inputModel.detectInput = false;
            actualSpeed = 0f;
        }

        private void Update() {
            UpdateMovement();

            if (inputModel.detectInput) {
                DetectMovement();

                DetectJump();

                DetectCrouch();

                DetectSprint();
            }
        }

        private void DetectMovement() {
            direction = transform.rotation * new Vector3(inputModel.horizontalMovementAxisRaw, 0, inputModel.verticalMovementAxisRaw);

            if (direction.magnitude > 1f)
                direction = direction.normalized;
        }

        private void DetectJump() {
            if (characterController.isGrounded && inputModel.jumpButtonDown)
                verticalVelocity = model.jumpVelocity;
        }

        private void DetectSprint() {
            if (isSprinting) {
                if (actualSpeed >= model.sprintSpeed)
                    actualSpeed = model.sprintSpeed;

                if (actualSpeed < model.sprintSpeed)
                    actualSpeed += Time.deltaTime * model.timeForSprint;
            }

            if (sprintStopping) {
                if (actualSpeed > model.moveSpeed)
                    actualSpeed -= Time.deltaTime * model.sprintSlowdownSpeed;

                if (actualSpeed <= model.moveSpeed) {
                    actualSpeed = model.moveSpeed;
                    sprintStopping = false;
                }
            }

            if (inputModel.sprintButtonDown) {
                isSprinting = true;

                if (isCrouching) {
                    isCrouching = false;
                    characterController.height = model.characterHeight;
                }
            }

            if (inputModel.sprintButtonUp) {
                isSprinting = false;
                sprintStopping = true;
            }
        }

        private void DetectCrouch() {
            if (inputModel.crouchButtonDown) {
                if (isSprinting)
                    isSprinting = false;

                if (isCrouching && GameDataController.Instance.ToggleCrouch) {
                    isCrouching = false;
                    actualSpeed = model.moveSpeed;
                    characterController.height = model.characterHeight;
                    return;
                }

                isCrouching = true;
                actualSpeed = model.crouchSpeed;
                characterController.height = model.characterHeightCrouching;
            }

            if (inputModel.crouchButtonUp && !GameDataController.Instance.ToggleCrouch) {
                isCrouching = false;
                actualSpeed = model.moveSpeed;
                characterController.height = model.characterHeight;
            }
        }

        private void UpdateMovement() {
            Vector3 distance = Vector3.zero;

            distance = direction * actualSpeed * Time.deltaTime;

            if (characterController.isGrounded && verticalVelocity < 0)
                verticalVelocity -= 0;
            else
                verticalVelocity += Physics.gravity.y * Time.deltaTime;

            distance.y = verticalVelocity * Time.deltaTime;
            characterController.Move(distance);

            model.actualSpeed = actualSpeed;
        }
    }
}
