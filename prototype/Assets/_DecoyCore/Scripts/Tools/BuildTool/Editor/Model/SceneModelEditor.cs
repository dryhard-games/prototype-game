namespace Decoy.Tools.BuildTool {
    using UnityEngine;
    using UnityEditor;
    using System.Collections.Generic;
    using Decoy.Core.LoadingSystem;
    /// <summary>
    /// SceneModelEditor.cs
    /// <summary>
    /// Author: Thomas van Opstal
    /// <summary>
    /// 
    /// </summary>
    [CustomEditor(typeof(SceneModel))]
    public class SceneModelEditor : Editor {
        private SceneModel model;

        public override void OnInspectorGUI() {
            model = (SceneModel)target;
            EditorGUILayout.LabelField("Scenes Configuration", EditorStyles.boldLabel, GUILayout.MaxWidth(200));
            GUILayout.Space(5);

            List<SceneModel.SceneBuildModel> sceneModels = new List<SceneModel.SceneBuildModel>();

            if (model.sceneModel != null) {
                foreach (SceneModel.SceneBuildModel sceneModel in model.sceneModel)
                    sceneModels.Add(sceneModel);
            }


            if (GUILayout.Button("Add Scene", GUILayout.MinWidth(200), GUILayout.MaxWidth(200)))
                sceneModels.Add(new SceneModel.SceneBuildModel());

            model.sceneModel = sceneModels.ToArray();

            for (int i = 0; i < model.sceneModel.Length; i++) {
                GUILayout.BeginVertical();
                GUILayout.Label(model.sceneModel[i].scene.ToString(), EditorStyles.whiteMiniLabel, GUILayout.MinWidth(100), GUILayout.MaxWidth(100));

                GUILayout.BeginHorizontal();
                GUILayout.Label("Scene: ", EditorStyles.label, GUILayout.MinWidth(200), GUILayout.MaxWidth(200));
                model.sceneModel[i].scene = (LoadingScreenController.Scenes)EditorGUILayout.EnumPopup(model.sceneModel[i].scene, GUILayout.MinWidth(200), GUILayout.MaxWidth(200));
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Label("Use Global Path: ", EditorStyles.label, GUILayout.MinWidth(200), GUILayout.MaxWidth(200));
                model.sceneModel[i].useGlobalScenePath = EditorGUILayout.Toggle(model.sceneModel[i].useGlobalScenePath, GUILayout.MinWidth(200), GUILayout.MaxWidth(200));
                GUILayout.EndHorizontal();

                if (!model.sceneModel[i].useGlobalScenePath) {
                    GUILayout.BeginHorizontal();
                    GUILayout.Label("Scene Path: ", EditorStyles.label, GUILayout.MinWidth(200), GUILayout.MaxWidth(200));
                    model.sceneModel[i].scenePath = EditorGUILayout.TextField(model.sceneModel[i].scenePath, GUILayout.MinWidth(200), GUILayout.MaxWidth(200));
                    if (GUILayout.Button("Browse", GUILayout.MaxWidth(75f)))
                        model.sceneModel[i].scenePath = EditorUtility.OpenFolderPanel("Path", "", "");
                    GUILayout.EndHorizontal();
                }

                if (GUILayout.Button("Remove Scene", GUILayout.MinWidth(200), GUILayout.MaxWidth(200)))
                    RemoveRequestedSceneModel(i);

                GUILayout.EndVertical();

                GUILayout.Space(5.0f);
            }

            GUILayout.BeginHorizontal();
            GUILayout.Label("Global Scene Path: ", EditorStyles.label, GUILayout.MinWidth(200), GUILayout.MaxWidth(200));
            model.globalScenePath = EditorGUILayout.TextField(model.globalScenePath, GUILayout.MinWidth(200), GUILayout.MaxWidth(200));
            if (GUILayout.Button("Browse", GUILayout.MaxWidth(75f)))
                model.globalScenePath = EditorUtility.OpenFolderPanel("Path", "", "");
            GUILayout.EndHorizontal();

            GUILayout.Space(5.0f);

            if (GUILayout.Button("Save Scene Config", GUILayout.MinWidth(200), GUILayout.MaxWidth(200))) {
                EditorUtility.SetDirty(model);
                AssetDatabase.SaveAssets();
            }
        }

        private void RemoveRequestedSceneModel(int index) {
            List<SceneModel.SceneBuildModel> sceneModels = new List<SceneModel.SceneBuildModel>();

            if (model.sceneModel != null) {
                foreach (SceneModel.SceneBuildModel sceneModel in model.sceneModel)
                    sceneModels.Add(sceneModel);
            }

            sceneModels.RemoveAt(index);

            model.sceneModel = sceneModels.ToArray();

            for (int i = 0; i < model.sceneModel.Length; i++) {
                GUILayout.BeginVertical();
                GUILayout.Label(model.sceneModel[i].scene.ToString(), EditorStyles.whiteMiniLabel, GUILayout.MinWidth(100), GUILayout.MaxWidth(100));

                GUILayout.BeginHorizontal();
                GUILayout.Label("Scene: ", EditorStyles.label, GUILayout.MinWidth(200), GUILayout.MaxWidth(200));
                model.sceneModel[i].scene = (LoadingScreenController.Scenes)EditorGUILayout.EnumPopup(model.sceneModel[i].scene, GUILayout.MinWidth(200), GUILayout.MaxWidth(200));
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Label("Use Global Path: ", EditorStyles.label, GUILayout.MinWidth(200), GUILayout.MaxWidth(200));
                model.sceneModel[i].useGlobalScenePath = EditorGUILayout.Toggle(model.sceneModel[i].useGlobalScenePath, GUILayout.MinWidth(200), GUILayout.MaxWidth(200));
                GUILayout.EndHorizontal();

                if (!model.sceneModel[i].useGlobalScenePath) {
                    GUILayout.BeginHorizontal();
                    GUILayout.Label("Scene Path: ", EditorStyles.label, GUILayout.MinWidth(200), GUILayout.MaxWidth(200));
                    model.sceneModel[i].scenePath = EditorGUILayout.TextField(model.sceneModel[i].scenePath, GUILayout.MinWidth(200), GUILayout.MaxWidth(200));
                    if (GUILayout.Button("Browse", GUILayout.MaxWidth(75f)))
                        model.sceneModel[i].scenePath = EditorUtility.OpenFolderPanel("Path", "", "");
                    GUILayout.EndHorizontal();
                }

                if (GUILayout.Button("Remove Scene", GUILayout.MinWidth(200), GUILayout.MaxWidth(200)))
                    RemoveRequestedSceneModel(i);

                GUILayout.EndVertical();

                GUILayout.Space(5.0f);
            }
        }
    }
}
