namespace Decoy.Core.Engagementscreen {
    using System.Collections;
    using UnityEngine;
    using Decoy.Core.InputSystem;
    using Decoy.Core.LoadingSystem;
    using Decoy.Core.SaveSystem;
    using Decoy.Core.GameDataSystem;
    using Decoy.Core.EventSystem;
    using Decoy.Core.Message;
    /// <summary>
    /// EngagementScreenController.cs
    /// <summary>
    /// Author: Thomas van Opstal
    /// </summary>
    public class EngagementScreenController : MonoBehaviour {
        [Header("Next Scene")]
        public LoadingScreenController.Scenes sceneToLoad;
        public InputModel inputModel;
        public float timeOut = 3.0f;

        private bool initializing;
        private bool savegameReady;
        private bool gamedataReady;

        private void Start() {
            EventManager.StartListening(SaveGameEventTypes.SAVE_GAME_READY, SaveGameReady);
            EventManager.StartListening(GameDataEventTypes.GAME_DATA_READY, GameDataReady);
        }

        private void OnDestroy() {
            EventManager.StopListening(SaveGameEventTypes.SAVE_GAME_READY, SaveGameReady);
            EventManager.StopListening(GameDataEventTypes.GAME_DATA_READY, GameDataReady);
        }

        private void Update() {
            if (inputModel.anyButtonDown && !initializing) {
                initializing = true;
                StartCoroutine(InitializePlayerSystems());
            }
        }

        private IEnumerator InitializePlayerSystems() {
            SaveController SaveInit = SaveController.Instance;
            EventManager.TriggerEvent(EngagementEventTypes.CHANGE_STATUS_TEXT, EngagementScreenStatusTypes.INITIALIZING_SAVE_SYSTEM);

            float timeLeft = timeOut;

            while (!savegameReady) {
                timeLeft -= Time.deltaTime;

                if (timeLeft <= 0) {
                    EventManager.TriggerEvent(MessageEventTypes.DISPLAY_MESSAGE, MessageTypes.ERROR, Messages.Failed_Init_Save);
                    break;
                }

                yield return null;
            }

            GameDataController GameDataInit = GameDataController.Instance;
            EventManager.TriggerEvent(EngagementEventTypes.CHANGE_STATUS_TEXT, EngagementScreenStatusTypes.INITIALIZING_GAMEDATA_SYSTEM);

            timeLeft = timeOut;

            while (!gamedataReady) {
                timeLeft -= Time.deltaTime;

                if (timeLeft <= 0) {
                    EventManager.TriggerEvent(MessageEventTypes.DISPLAY_MESSAGE, MessageTypes.ERROR, Messages.Failed_Init_GameData);
                    break;
                }

                yield return null;
            }

            EventManager.TriggerEvent(EngagementEventTypes.CHANGE_STATUS_TEXT, EngagementScreenStatusTypes.INITIALIZING_STEAM_SYSTEM);
            timeLeft = timeOut;

            while (!SteamManager.Initialized) {
                timeLeft -= Time.deltaTime;

                if (timeLeft <= 0) {
                    EventManager.TriggerEvent(MessageEventTypes.DISPLAY_MESSAGE, MessageTypes.ERROR, Messages.Failed_Init_Steam);
                    break;
                }

                yield return null;
            }

            EventManager.TriggerEvent(EngagementEventTypes.CHANGE_STATUS_TEXT, EngagementScreenStatusTypes.INITIALIZATION_COMPLETED);
            yield return null;

            LoadingScreenController.Instance.LoadScene(LoadingScreenController.Scenes.MainMenuScreen);
        }

        private void SaveGameReady(object[] arg0) {
            savegameReady = true;
        }

        private void GameDataReady(object[] arg0) {
            gamedataReady = true;
        }
    }
}
