namespace Decoy.Core.GameDataSystem {
    using UnityEngine;
    /// <summary>
    /// GameDataModel.cs
    /// <summary>
    /// Author: Thomas van Opstal
    /// </summary>
    [CreateAssetMenu(fileName = "GameDataModel", menuName = "Decoy/GameData/New GameData Model", order = 0)]
    public class GameDataModel : ScriptableObject {
        [Header("Graphics Settings")]
        public bool fullscreen = false;
        public bool vSync = false;
        public bool anisotropicFiltering = true;
        [Tooltip("0 - Low, 5 - Ultra")]
        [Range(0, 5)]
        public int preset = 3;
        [Tooltip("0 - Low, 2 - High")]
        [Range(0, 2)]
        public int textureQuality = 2;
        [Tooltip("0 - Off, 2 - High")]
        [Range(0, 2)]
        public int shadowQuality = 2;
        [Tooltip("0 - 0x, 4 - 8x")]
        [Range(0, 4)]
        public int AA = 0;
        [Tooltip("Field of View")]
        [Range(60, 100)]
        public int FOV = 75;
        public string aspectRatio = "16, 9";
        [Space(5)]

        [Header("Default Audio Settings")]
        [Range(0, 1)]
        public float musicVolume = 0.5f;
        [Range(0, 1)]
        public float sfxVolume = 0.5f;
        [Space(5)]

        [Header("Default Gameplay Settings")]
        [Range(0, 1)]
        public float mouseSensitivity = 0.5f;
        [Range(0, 5)]
        public float controllerSensitivity = 2.5f;
        public bool mouseYInverted = false;
        public bool controllerYInverted = false;
        public bool mouseSmoothing = false;
        public bool toggleCrouch = false;
    }
}
