namespace Decoy.Tools.BuildTool {
    using UnityEngine;
    using UnityEditor;
    /// <summary>
    /// BuildModelCustomEditor.cs
    /// <summary>
    /// Author: Thomas van Opstal
    /// <summary>
    /// 
    /// </summary>
    [CustomEditor(typeof(BuildModel))]
    public class BuildModelEditor : Editor {
        private BuildModel model;

        public override void OnInspectorGUI() {
            model = (BuildModel)target;

            EditorGUILayout.LabelField("Build Configuration", EditorStyles.boldLabel, GUILayout.MaxWidth(200));
            GUILayout.Space(5);

            GUILayout.BeginVertical();
            GUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Fullscreen Mode", EditorStyles.label, GUILayout.MinWidth(200), GUILayout.MaxWidth(200));
            model.buildModel.fullscreenMode = (FullScreenMode)EditorGUILayout.EnumPopup(model.buildModel.fullscreenMode, GUILayout.MinWidth(200), GUILayout.MaxWidth(200));
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Default is native Res", EditorStyles.label, GUILayout.MinWidth(200), GUILayout.MaxWidth(200));
            model.buildModel.defaultIsNativeResolution = EditorGUILayout.Toggle(model.buildModel.defaultIsNativeResolution, GUILayout.MinWidth(200), GUILayout.MaxWidth(200));
            GUILayout.EndHorizontal();

            if (!model.buildModel.defaultIsNativeResolution) {
                GUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Default Resolution", EditorStyles.label, GUILayout.MinWidth(200), GUILayout.MaxWidth(200));
                EditorGUILayout.LabelField("W:", EditorStyles.label, GUILayout.MinWidth(25), GUILayout.MaxWidth(25));
                model.buildModel.defaultResolution.x = EditorGUILayout.IntField((int)model.buildModel.defaultResolution.x, GUILayout.MinWidth(75), GUILayout.MaxWidth(75));
                EditorGUILayout.LabelField("H:", EditorStyles.label, GUILayout.MinWidth(25), GUILayout.MaxWidth(25));
                model.buildModel.defaultResolution.y = EditorGUILayout.IntField((int)model.buildModel.defaultResolution.y, GUILayout.MinWidth(75), GUILayout.MaxWidth(75));
                GUILayout.EndHorizontal();
            }

            GUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Retina Support", EditorStyles.label, GUILayout.MinWidth(200), GUILayout.MaxWidth(200));
            model.buildModel.macRetinaSupport = EditorGUILayout.Toggle(model.buildModel.macRetinaSupport, GUILayout.MinWidth(200), GUILayout.MaxWidth(200));
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Run in Background", EditorStyles.label, GUILayout.MinWidth(200), GUILayout.MaxWidth(200));
            model.buildModel.runInBackground = EditorGUILayout.Toggle(model.buildModel.runInBackground, GUILayout.MinWidth(200), GUILayout.MaxWidth(200));
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Capture single screen", EditorStyles.label, GUILayout.MinWidth(200), GUILayout.MaxWidth(200));
            model.buildModel.captureSingleScreen = EditorGUILayout.Toggle(model.buildModel.captureSingleScreen, GUILayout.MinWidth(200), GUILayout.MaxWidth(200));
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Res Display Setting", EditorStyles.label, GUILayout.MinWidth(200), GUILayout.MaxWidth(200));
            model.buildModel.resolutionDialogSetting = (ResolutionDialogSetting)EditorGUILayout.EnumPopup(model.buildModel.resolutionDialogSetting, GUILayout.MinWidth(200), GUILayout.MaxWidth(200));
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Use Player Log", EditorStyles.label, GUILayout.MinWidth(200), GUILayout.MaxWidth(200));
            model.buildModel.usePlayerLog = EditorGUILayout.Toggle(model.buildModel.usePlayerLog, GUILayout.MinWidth(200), GUILayout.MaxWidth(200));
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Resizable Window", EditorStyles.label, GUILayout.MinWidth(200), GUILayout.MaxWidth(200));
            model.buildModel.resizableWindow = EditorGUILayout.Toggle(model.buildModel.resizableWindow, GUILayout.MinWidth(200), GUILayout.MaxWidth(200));
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Visible in Background", EditorStyles.label, GUILayout.MinWidth(200), GUILayout.MaxWidth(200));
            model.buildModel.visibleInBackground = EditorGUILayout.Toggle(model.buildModel.visibleInBackground, GUILayout.MinWidth(200), GUILayout.MaxWidth(200));
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Allow Fullscreen Switch", EditorStyles.label, GUILayout.MinWidth(200), GUILayout.MaxWidth(200));
            model.buildModel.allowFullscreenSwitch = EditorGUILayout.Toggle(model.buildModel.allowFullscreenSwitch, GUILayout.MinWidth(200), GUILayout.MaxWidth(200));
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Force Single Instance", EditorStyles.label, GUILayout.MinWidth(200), GUILayout.MaxWidth(200));
            model.buildModel.forceSingleInstance = EditorGUILayout.Toggle(model.buildModel.forceSingleInstance, GUILayout.MinWidth(200), GUILayout.MaxWidth(200));
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Show Splash", EditorStyles.label, GUILayout.MinWidth(200), GUILayout.MaxWidth(200));
            model.buildModel.showSplashscreen = EditorGUILayout.Toggle(model.buildModel.showSplashscreen, GUILayout.MinWidth(200), GUILayout.MaxWidth(200));
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();

            if (GUILayout.Button("Save Build Config", GUILayout.MinWidth(200), GUILayout.MaxWidth(200))) {
                EditorUtility.SetDirty(model);
                AssetDatabase.SaveAssets();
            }

            GUILayout.Space(10);
        }
    }
}
