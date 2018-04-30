namespace Decoy.Core.Splashscreen {
    using UnityEngine;
    using System.Collections;
    using Decoy.Core.LoadingSystem;
    /// <summary>
    /// SplashScreenController.cs
    /// <summary>
    /// Author: Thomas van Opstal
    /// <summary>
    /// Main controller for the splashscreen of your game.
    /// </summary>
    public class SplashScreenController : MonoBehaviour {
        [Header("Time(in seconds) before loading next scene.")]
        public float time = 2.0f;

        [Header("Scene to be Loaded")]
        public LoadingScreenController.Scenes sceneIndex;

        private void Start() {
            StartCoroutine(GoToNextScene());
        }

        private IEnumerator GoToNextScene() {
            yield return new WaitForSeconds(time);

            LoadingScreenController.Instance.LoadScene(sceneIndex);
        }
    }
}
