namespace Decoy.Core.Player {
    using UnityEngine;
    /// <summary>
    /// PlayerModel.cs
    /// <summary>
    /// Author: Thomas van Opstal
    /// <summary>
    /// Data the player always needs.
    /// </summary>
    [CreateAssetMenu(fileName = "PlayerModel", menuName = "Decoy/Player/New Player Model")]
    public class PlayerModel : ScriptableObject {
        [Header("Movement Properties")]
        public float actualSpeed;
        public float moveSpeed;
        public float crouchSpeed;
        public float sprintSpeed;
        public float jumpVelocity;
        public float timeForSprint;
        public float sprintSlowdownSpeed;

        [Header("Camera Properties")]
        public Vector2 cameraClamp = new Vector2(360, 180);
        public float cameraSmoothing = 1.3f;

        [Header("Character Settings")]
        public float characterHeight;
        public float characterHeightCrouching;

        private void OnEnable() {
            actualSpeed = 0;
        }
    }
}
