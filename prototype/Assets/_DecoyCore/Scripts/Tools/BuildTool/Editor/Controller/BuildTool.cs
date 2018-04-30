namespace Decoy.Tools.BuildTool {
    using UnityEngine;
    using UnityEditor;
    /// <summary>
    /// TotalBuildTool.cs
    /// <summary>
    /// Author: Thomas van Opstal
    /// <summary>
    /// Builds for selected platform.
    /// </summary>
    public class BuildTool : EditorWindow {
        public BuildModel buildSettings;
        public PlatformModel platformSettings;
        public SceneModel sceneSettings;
        public Texture cTexture;
        private bool isBuilding;
        private bool canBuild;

        [MenuItem("Decoy Tools/Build Tool")]
        private static void Init() {
            BuildTool window = (BuildTool)GetWindow(typeof(BuildTool), true, "Build Tool");
            window.maxSize = new Vector2(300f, 130f);
            window.minSize = window.maxSize;
            window.Show();
        }

        private void OnGUI() {
            EditorGUILayout.LabelField("Create builds for multiple platforms", EditorStyles.miniBoldLabel);
            GUILayout.Space(5);

            GUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Build Model: ", EditorStyles.boldLabel, GUILayout.MaxWidth(110));
            buildSettings = (BuildModel)EditorGUILayout.ObjectField(buildSettings, typeof(BuildModel), false, GUILayout.MaxWidth(200));
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Platform Model: ", EditorStyles.boldLabel, GUILayout.MaxWidth(110));
            platformSettings = (PlatformModel)EditorGUILayout.ObjectField(platformSettings, typeof(PlatformModel), false, GUILayout.MaxWidth(200));
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Scene Model: ", EditorStyles.boldLabel, GUILayout.MaxWidth(110));
            sceneSettings = (SceneModel)EditorGUILayout.ObjectField(sceneSettings, typeof(SceneModel), false, GUILayout.MaxWidth(200));
            GUILayout.EndHorizontal();

            GUILayout.Space(10);

            if (GUILayout.Button("Build")) {
                if (!canBuild)
                    return;

                BuildGame();
            }

            if (buildSettings == null || platformSettings == null || sceneSettings == null) {
                EditorGUILayout.LabelField("Select the proper configs!", EditorStyles.miniBoldLabel, GUILayout.MaxWidth(200));
                canBuild = false;
            } else {
                EditorGUILayout.LabelField("You can now build!", EditorStyles.miniBoldLabel, GUILayout.MaxWidth(200));
                canBuild = true;
            }
        }

        private void BuildGame() {
            BuildTarget target = EditorUserBuildSettings.selectedStandaloneTarget;
            BuildTargetGroup targetGroup = EditorUserBuildSettings.selectedBuildTargetGroup;

            PlayerSettings.fullScreenMode = buildSettings.buildModel.fullscreenMode;
            PlayerSettings.defaultIsNativeResolution = buildSettings.buildModel.defaultIsNativeResolution;

            if (!buildSettings.buildModel.defaultIsNativeResolution) {
                PlayerSettings.defaultScreenHeight = (int)buildSettings.buildModel.defaultResolution.y;
                PlayerSettings.defaultScreenWidth = (int)buildSettings.buildModel.defaultResolution.x;
            }

            PlayerSettings.macRetinaSupport = buildSettings.buildModel.macRetinaSupport;
            PlayerSettings.runInBackground = buildSettings.buildModel.runInBackground;
            PlayerSettings.captureSingleScreen = buildSettings.buildModel.captureSingleScreen;
            PlayerSettings.displayResolutionDialog = buildSettings.buildModel.resolutionDialogSetting;
            PlayerSettings.usePlayerLog = buildSettings.buildModel.usePlayerLog;
            PlayerSettings.resizableWindow = buildSettings.buildModel.resizableWindow;
            PlayerSettings.visibleInBackground = buildSettings.buildModel.visibleInBackground;
            PlayerSettings.allowFullscreenSwitch = buildSettings.buildModel.allowFullscreenSwitch;
            PlayerSettings.forceSingleInstance = buildSettings.buildModel.forceSingleInstance;
            PlayerSettings.SplashScreen.show = Application.HasProLicense() ? buildSettings.buildModel.showSplashscreen : true;

            string[] scenePaths = new string[sceneSettings.sceneModel.Length];

            for (int i = 0; i < sceneSettings.sceneModel.Length; i++)
                scenePaths[i] = sceneSettings.sceneModel[i].useGlobalScenePath ? (sceneSettings.globalScenePath + "/" + sceneSettings.sceneModel[i].scene.ToString() + ".unity") : (sceneSettings.sceneModel[i].scenePath + "/" + sceneSettings.sceneModel[i].scene.ToString() + ".unity");

            for (int i = 0; i < platformSettings.platformModel.Length; i++) {
                string buildPath = platformSettings.platformModel[i].buildPath;
                buildPath = (platformSettings.platformModel[i].extension == PlatformModel.EXTENSION.NONE) ? buildPath + "/" + platformSettings.platformModel[i].applicationName : buildPath + "/" + platformSettings.platformModel[i].applicationName + "." + platformSettings.platformModel[i].extension.ToString();

                BuildPlayerOptions buildOptions = new BuildPlayerOptions {
                    target = platformSettings.platformModel[i].platform,
                    locationPathName = buildPath,
                    scenes = scenePaths
                };

                BuildPipeline.BuildPlayer(buildOptions);

                while (isBuilding)
                    isBuilding = BuildPipeline.isBuildingPlayer;
            }

            //Return to working platform
            EditorUserBuildSettings.SwitchActiveBuildTarget(targetGroup, target);
        }
    }
}
