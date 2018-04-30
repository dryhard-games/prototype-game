namespace Decoy.Tools.BuildTool {
    using UnityEngine;
    using UnityEditor;
    using System;
    /// <summary>
    /// BuildModel.cs
    /// <summary>
    /// Author: Thomas van Opstal
    /// <summary>
    /// BuildModel
    /// </summary>
    [CreateAssetMenu(fileName = "BuildModel", menuName = "Decoy/Build Configuration/BuildConfigModel", order = 0)]
    public class BuildModel : ScriptableObject {
        [Serializable]
        public struct BuildConfigModel {
            public FullScreenMode fullscreenMode;
            public bool defaultIsNativeResolution;
            public Vector2 defaultResolution;
            public bool macRetinaSupport;
            public bool runInBackground;
            public bool captureSingleScreen;
            public ResolutionDialogSetting resolutionDialogSetting;
            public bool usePlayerLog;
            public bool resizableWindow;
            public bool visibleInBackground;
            public bool allowFullscreenSwitch;
            public bool forceSingleInstance;
            public bool showSplashscreen;
        }

        public BuildConfigModel buildModel;
    }
}
