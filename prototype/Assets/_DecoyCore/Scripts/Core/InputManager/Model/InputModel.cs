namespace Decoy.Core.InputSystem {
    using UnityEngine;
    /// <summary>
    /// InputModel.cs
    /// <summary>
    /// Author: Thomas van Opstal
    /// <summary>
    /// 
    /// </summary>
    [CreateAssetMenu(fileName = "InputModel", menuName = "Decoy/Input/New Input Model", order = 0)]
    public class InputModel : ScriptableObject {
        [Header("Gamepad Settings")]
        public bool gamepadActive;

        [Header("Axis Input")]
        [Header("Look Axis")]
        public float horizontalLookAxisRaw;
        public float verticalLookAxisRaw;
        public float horizontalLookAxis;
        public float verticalLookAxis;
        [Header("Movement Axis")]
        public float horizontalMovementAxisRaw;
        public float verticalMovementAxisRaw;
        public float horizontalMovementAxis;
        public float verticalMovementAxis;

        [Header("Buttons")]
        [Header("Button Up")]
        public bool sprintButtonUp;
        public bool crouchButtonUp;
        public bool jumpButtonUp;
        [Header("Button Down")]
        public bool sprintButtonDown;
        public bool crouchButtonDown;
        public bool jumpButtonDown;
        [Header("Button Hold")]
        public bool sprintButtonHold;
        public bool crouchButtonHold;
        public bool jumpButtonHold;

        [Header("General Button")]
        public bool anyButtonDown;

        [Header("Detect Input")]
        public bool detectInput;

        private void OnEnable() {
            detectInput = true;
        }
    }
}
