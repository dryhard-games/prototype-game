namespace Decoy.Tools.BuildTool {
    using UnityEngine;
    using System;
    using Decoy.Core.LoadingSystem;
    /// <summary>
    /// SceneModel.cs
    /// <summary>
    /// Author: Thomas van Opstal
    /// <summary>
    /// SceneModel
    /// </summary>
    [CreateAssetMenu(fileName = "SceneModel", menuName = "Decoy/Build Configuration/SceneConfigModel", order = 2)]
    public class SceneModel : ScriptableObject {
        [Serializable]
        public struct SceneBuildModel {
            public LoadingScreenController.Scenes scene;
            public bool useGlobalScenePath;
            public string scenePath;
        }

        public SceneBuildModel[] sceneModel;
        public string globalScenePath;
    }
}
