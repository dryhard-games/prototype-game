namespace Decoy.Tools.BuildTool {
    using UnityEngine;
    using UnityEditor;
    using System;
    /// <summary>
    /// PlatformModel.cs
    /// <summary>
    /// Author: Thomas van Opstal
    /// <summary>
    /// PlatformModel
    /// </summary>
    [CreateAssetMenu(fileName = "Platform Model", menuName = "Decoy/Build Configuration/PlatformConfigModel", order = 1)]
    public class PlatformModel : ScriptableObject {
        public enum EXTENSION {
            NONE,
            exe,
            x86,
            x86_64
        }

        [Serializable]
        public struct PlatformConfigModel {
            public BuildTarget platform;
            public string buildPath;
            public string applicationName;
            public EXTENSION extension;
        }

        public PlatformConfigModel[] platformModel;
    }
}
