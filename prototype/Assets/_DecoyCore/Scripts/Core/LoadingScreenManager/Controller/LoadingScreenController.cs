namespace Decoy.Core.LoadingSystem {
    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.SceneManagement;
    using System.Collections;
    /// <summary>
    /// LoadingScreenController.cs
    /// <summary>
    /// Author: Thomas van Opstal
    /// <summary>
    /// Main controller for the loading screen.
    /// </summary>
    public class LoadingScreenController : MonoBehaviour {
        //These Enum represent the scenes set in the Build Settings.
        public enum Scenes {
            LoadingScreen,
            SplashScreen,
            EngagementScreen,
            MainMenuScreen,
            SettingsScreen,
            Game,
        }

        private static Slider loadingBar;
        private static Scenes sceneToBeLoaded;
        private static AsyncOperation asyncLoad;
        private static LoadingScreenController controller;

        public static LoadingScreenController Instance {
            get {
                if (!controller) {
                    controller = FindObjectOfType(typeof(LoadingScreenController)) as LoadingScreenController;

                    if (!controller)
                        Debug.Log("There needs to be one active LoadingScreenController active in your scene");
                }
                return controller;
            }
        }

        /// <summary>
        /// Request a scene to be loaded
        /// </summary>
        /// <param name="scene">(Scenes)Requested Scene</param>
        public void LoadScene(Scenes scene) {
            sceneToBeLoaded = scene;
            SceneManager.LoadScene(Scenes.LoadingScreen.ToString());

            StartCoroutine(LoadRequestedScene());
        }

        private IEnumerator LoadRequestedScene() {
            //Wait For Loading Screen
            while (SceneManager.GetActiveScene().name != Scenes.LoadingScreen.ToString())
                yield return null;

            //Get the loadingbar to represent progress.
            loadingBar = GameObject.Find("LoadingBar").GetComponent<Slider>();

            asyncLoad = SceneManager.LoadSceneAsync(sceneToBeLoaded.ToString());
            asyncLoad.allowSceneActivation = false;

            while (asyncLoad != null) {
                loadingBar.value = asyncLoad.progress * 111.0f;

                if (asyncLoad.progress >= 0.9f) {
                    asyncLoad.allowSceneActivation = true;
                    asyncLoad = null;
                }
            }
        }
    }
}
