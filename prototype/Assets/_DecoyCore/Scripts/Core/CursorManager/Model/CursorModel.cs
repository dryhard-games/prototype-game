namespace Decoy.Core.CursorManager {
    using UnityEngine;
    using Decoy.Core.LoadingSystem;
    using System;
    /// <summary>
    /// CursorModel.cs
    /// <summary>
    /// Author: Thomas van Opstal
    /// </summary>
    [CreateAssetMenu(fileName = "CursorModel", menuName = "Decoy/Cursor/New Cursor Model", order = 0)]
    public class CursorModel : ScriptableObject {
        [Serializable]
        public struct CursorSetting {
            public LoadingScreenController.Scenes scene;
            public CursorLockMode cursorLockMode;
            public bool isVisible;
        }

        public CursorSetting[] cursorSettings;
    }
}
