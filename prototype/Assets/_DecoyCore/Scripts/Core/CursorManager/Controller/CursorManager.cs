namespace Decoy.Core.CursorManager {
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using Decoy.Core.LoadingSystem;
    using Decoy.Core.EventSystem;
    /// <summary>
    /// CursorManager.cs
    /// <summary>
    /// Author: Thomas van Opstal
    /// </summary>
    public class CursorManager : MonoBehaviour {
        public CursorModel cursorModel;

        //Default values
        private CursorLockMode lockMode = CursorLockMode.Confined;
        private bool isVisible = true;

        private void Start() {
            EventManager.StartListening(CursorEventTypes.CHANGE_CURSOR_SETTINGS, ChangeCursorSettings);
            EventManager.StartListening(CursorEventTypes.RETURN_TO_DEFAULT_SETTINGS, ReturnToDefaultSettings);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDestroy() {
            EventManager.StopListening(CursorEventTypes.CHANGE_CURSOR_SETTINGS, ChangeCursorSettings);
            EventManager.StopListening(CursorEventTypes.RETURN_TO_DEFAULT_SETTINGS, ReturnToDefaultSettings);
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void Update() {
            Cursor.lockState = lockMode;
            Cursor.visible = isVisible;
        }

        private void ChangeCursorSettings(object[] data) {
            lockMode = (CursorLockMode)data[0];
            isVisible = (bool)data[1];
        }

        private void ReturnToDefaultSettings(object[] arg0) {
            foreach (CursorModel.CursorSetting setting in cursorModel.cursorSettings) {
                if (setting.scene != (LoadingScreenController.Scenes)SceneManager.GetActiveScene().buildIndex)
                    continue;

                lockMode = setting.cursorLockMode;
                isVisible = setting.isVisible;
                break;
            }
        }

        private void OnSceneLoaded(Scene Scene, LoadSceneMode LoadMode) {
            foreach (CursorModel.CursorSetting setting in cursorModel.cursorSettings) {
                if (setting.scene != (LoadingScreenController.Scenes)Scene.buildIndex)
                    continue;

                lockMode = setting.cursorLockMode;
                isVisible = setting.isVisible;
                break;
            }
        }
    }
}
