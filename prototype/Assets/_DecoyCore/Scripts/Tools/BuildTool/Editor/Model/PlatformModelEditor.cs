namespace Decoy.Tools.BuildTool {
    using UnityEngine;
    using UnityEditor;
    using System.Collections.Generic;
    /// <summary>
    /// PlatformModelEditor.cs
    /// <summary>
    /// Author: Thomas van Opstal
    /// <summary>
    /// 
    /// </summary>
    [CustomEditor(typeof(PlatformModel))]
    public class PlatformModelEditor : Editor {
        private PlatformModel model;

        public override void OnInspectorGUI() {
            model = (PlatformModel)target;
            EditorGUILayout.LabelField("Platform Configurations", EditorStyles.boldLabel, GUILayout.MaxWidth(200));
            GUILayout.Space(5);

            List<PlatformModel.PlatformConfigModel> platformModels = new List<PlatformModel.PlatformConfigModel>();

            if (model.platformModel != null) {
                foreach (PlatformModel.PlatformConfigModel platformModel in model.platformModel)
                    platformModels.Add(platformModel);
            }


            if (GUILayout.Button("Add Platform", GUILayout.MinWidth(200), GUILayout.MaxWidth(200)))
                platformModels.Add(new PlatformModel.PlatformConfigModel());

            model.platformModel = platformModels.ToArray();

            for (int i = 0; i < model.platformModel.Length; i++) {
                GUILayout.BeginVertical();
                GUILayout.Label(model.platformModel[i].platform.ToString(), EditorStyles.whiteMiniLabel, GUILayout.MinWidth(100), GUILayout.MaxWidth(100));

                GUILayout.BeginHorizontal();
                GUILayout.Label("Platform: ", EditorStyles.label, GUILayout.MinWidth(200), GUILayout.MaxWidth(200));
                model.platformModel[i].platform = (BuildTarget)EditorGUILayout.EnumPopup(model.platformModel[i].platform, GUILayout.MinWidth(200), GUILayout.MaxWidth(200));
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Label("Application Name: ", EditorStyles.label, GUILayout.MinWidth(200), GUILayout.MaxWidth(200));
                model.platformModel[i].applicationName = EditorGUILayout.TextField(model.platformModel[i].applicationName, GUILayout.MinWidth(200), GUILayout.MaxWidth(200));
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Label("Build Path: ", EditorStyles.label, GUILayout.MinWidth(200), GUILayout.MaxWidth(200));
                model.platformModel[i].buildPath = EditorGUILayout.TextField(model.platformModel[i].buildPath, GUILayout.MinWidth(200), GUILayout.MaxWidth(200));
                if (GUILayout.Button("Browse", GUILayout.MaxWidth(75f)))
                    model.platformModel[i].buildPath = EditorUtility.OpenFolderPanel("Path", "", "");
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Label("File Extension: ", EditorStyles.label, GUILayout.MinWidth(200), GUILayout.MaxWidth(200));
                model.platformModel[i].extension = (PlatformModel.EXTENSION)EditorGUILayout.EnumPopup(model.platformModel[i].extension, GUILayout.MinWidth(200), GUILayout.MaxWidth(200));
                GUILayout.EndHorizontal();

                if (GUILayout.Button("Remove Platform", GUILayout.MinWidth(200), GUILayout.MaxWidth(200)))
                    RemoveRequestedPlatformModel(i);

                GUILayout.EndVertical();

                GUILayout.Space(5.0f);
            }


            if (GUILayout.Button("Save Platform Config", GUILayout.MinWidth(200), GUILayout.MaxWidth(200))) {
                EditorUtility.SetDirty(model);
                AssetDatabase.SaveAssets();
            }

            GUILayout.Space(10);
        }

        private void RemoveRequestedPlatformModel(int index) {
            List<PlatformModel.PlatformConfigModel> platformModels = new List<PlatformModel.PlatformConfigModel>();

            if (model.platformModel != null) {
                foreach (PlatformModel.PlatformConfigModel platformModel in model.platformModel)
                    platformModels.Add(platformModel);
            }

            platformModels.RemoveAt(index);

            model.platformModel = platformModels.ToArray();

            for (int i = 0; i < model.platformModel.Length; i++) {
                GUILayout.BeginVertical();
                GUILayout.Label(model.platformModel[i].platform.ToString(), EditorStyles.whiteMiniLabel, GUILayout.MinWidth(100), GUILayout.MaxWidth(100));

                GUILayout.BeginHorizontal();
                GUILayout.Label("Platform: ", EditorStyles.label, GUILayout.MinWidth(200), GUILayout.MaxWidth(200));
                model.platformModel[i].platform = (BuildTarget)EditorGUILayout.EnumPopup(model.platformModel[i].platform, GUILayout.MinWidth(200), GUILayout.MaxWidth(200));
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Label("Build Path: ", EditorStyles.label, GUILayout.MinWidth(200), GUILayout.MaxWidth(200));
                model.platformModel[i].buildPath = EditorGUILayout.TextField(model.platformModel[i].buildPath, GUILayout.MinWidth(200), GUILayout.MaxWidth(200));
                if (GUILayout.Button("Browse", GUILayout.MaxWidth(75f)))
                    model.platformModel[i].buildPath = EditorUtility.OpenFolderPanel("Path", "", "");
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Label("File Extension: ", EditorStyles.label, GUILayout.MinWidth(200), GUILayout.MaxWidth(200));
                model.platformModel[i].extension = (PlatformModel.EXTENSION)EditorGUILayout.EnumPopup(model.platformModel[i].extension, GUILayout.MinWidth(200), GUILayout.MaxWidth(200));
                GUILayout.EndHorizontal();

                if (GUILayout.Button("Remove Platform", GUILayout.MinWidth(200), GUILayout.MaxWidth(200)))
                    RemoveRequestedPlatformModel(i);

                GUILayout.EndVertical();

                GUILayout.Space(5.0f);
            }
        }
    }
}
