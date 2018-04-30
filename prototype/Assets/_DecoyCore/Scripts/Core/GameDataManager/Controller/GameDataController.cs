namespace Decoy.Core.GameDataSystem {
    using UnityEngine;
    using Decoy.Core.EventSystem;
    using Decoy.Core.SaveSystem;
    using Decoy.Core.Utilities;
    using Decoy.Core.InputSystem;
    using System;
    /// <summary>
    /// GameDataController.cs
    /// <summary>
    /// Author: Thomas van Opstal
    /// </summary>
    public class GameDataController : MonoBehaviour {
        [Tooltip("Default settings")]
        public GameDataModel defaultSettingsModel;
        public GameDataModel currentSettingsModel;

        private static GameDataController manager;

        public static GameDataController Instance {
            get {
                if (!manager) {
                    manager = FindObjectOfType(typeof(GameDataController)) as GameDataController;

                    if (!manager)
                        Debug.LogError("There needs to be one active GameDataController active in your scene.");
                    else
                        manager.Init();
                }
                return manager;
            }
        }

        private void Init() {
            LoadDataFromSaveFile(null);
        }

        private void Start() {
            EventManager.StartListening(GameDataEventTypes.LOAD_DEFAULT_SETTINGS, LoadDefaultSettings);
            EventManager.StartListening(GameDataEventTypes.LOAD_DEFAULT_AUDIO, LoadDefaultAudioSettings);
            EventManager.StartListening(GameDataEventTypes.LOAD_DEFAULT_GRAPHICS, LoadDefaultGraphicsSettings);
            EventManager.StartListening(GameDataEventTypes.LOAD_DEFAULT_GAMEPLAY, LoadDefaultGameplaySettings);
            EventManager.StartListening(GameDataEventTypes.LOAD_DATA_FROM_SAVE, LoadDataFromSaveFile);
        }

        private void OnDestroy() {
            EventManager.StopListening(GameDataEventTypes.LOAD_DEFAULT_SETTINGS, LoadDefaultSettings);
            EventManager.StopListening(GameDataEventTypes.LOAD_DEFAULT_AUDIO, LoadDefaultAudioSettings);
            EventManager.StopListening(GameDataEventTypes.LOAD_DEFAULT_GRAPHICS, LoadDefaultGraphicsSettings);
            EventManager.StopListening(GameDataEventTypes.LOAD_DEFAULT_GAMEPLAY, LoadDefaultGameplaySettings);
            EventManager.StopListening(GameDataEventTypes.LOAD_DATA_FROM_SAVE, LoadDataFromSaveFile);
        }

        #region Graphics Settings
        private Vector2 resolution;
        public Vector2 Resolution {
            get { return resolution; }
            set {
                resolution = value;
                SaveController.Instance.WriteAndSaveData(SaveTypes.Graphics, SaveTypes.Resolution, resolution.ToString());
            }
        }

        private Vector2 aspectRatio;
        public Vector2 AspectRatio {
            get { return aspectRatio; }
            set {
                aspectRatio = value;
                SaveController.Instance.WriteAndSaveData(SaveTypes.Graphics, SaveTypes.AspectRatio, aspectRatio.ToString());
            }
        }

        public bool Fullscreen {
            get { return currentSettingsModel.fullscreen; }
            set {
                currentSettingsModel.fullscreen = value;
                SaveController.Instance.WriteAndSaveData(SaveTypes.Graphics, SaveTypes.Fullscreen, currentSettingsModel.fullscreen.ToString());
            }
        }

        public bool VSync {
            get { return currentSettingsModel.vSync; }
            set {
                currentSettingsModel.vSync = value;
                SaveController.Instance.WriteAndSaveData(SaveTypes.Graphics, SaveTypes.Vsync, currentSettingsModel.vSync.ToString());
            }
        }

        public int Preset {
            get { return currentSettingsModel.preset; }
            set {
                currentSettingsModel.preset = value;
                SaveController.Instance.WriteAndSaveData(SaveTypes.Graphics, SaveTypes.Preset, currentSettingsModel.preset.ToString());
            }
        }

        public int TextureQuality {
            get { return currentSettingsModel.textureQuality; }
            set {
                currentSettingsModel.textureQuality = value;
                SaveController.Instance.WriteAndSaveData(SaveTypes.Graphics, SaveTypes.TextureQuality, currentSettingsModel.textureQuality.ToString());
            }
        }

        public int ShadowQuality {
            get { return currentSettingsModel.shadowQuality; }
            set {
                currentSettingsModel.shadowQuality = value;
                SaveController.Instance.WriteAndSaveData(SaveTypes.Graphics, SaveTypes.ShadowQuality, currentSettingsModel.shadowQuality.ToString());
            }
        }

        public int AA {
            get { return currentSettingsModel.AA; }
            set {
                currentSettingsModel.AA = value;
                SaveController.Instance.WriteAndSaveData(SaveTypes.Graphics, SaveTypes.AntiAliasing, currentSettingsModel.AA.ToString());
            }
        }

        public bool AnisotropicFiltering {
            get { return currentSettingsModel.anisotropicFiltering; }
            set {
                currentSettingsModel.anisotropicFiltering = value;
                SaveController.Instance.WriteAndSaveData(SaveTypes.Graphics, SaveTypes.AnisotropicFiltering, currentSettingsModel.anisotropicFiltering.ToString());
            }
        }

        public float FOV {
            get { return currentSettingsModel.FOV; }
            set {
                currentSettingsModel.FOV = (int)value;
                SaveController.Instance.WriteAndSaveData(SaveTypes.Graphics, SaveTypes.FieldOfView, currentSettingsModel.FOV.ToString());
            }
        }
        #endregion

        #region Audio Settings
        public float MusicVolume {
            get { return currentSettingsModel.musicVolume; }
            set {
                currentSettingsModel.musicVolume = value;
                SaveController.Instance.WriteAndSaveData(SaveTypes.Audio, SaveTypes.MusicVolume, currentSettingsModel.musicVolume.ToString());
            }
        }

        public float SoundEffectVolume {
            get { return currentSettingsModel.sfxVolume; }
            set {
                currentSettingsModel.sfxVolume = value;
                SaveController.Instance.WriteAndSaveData(SaveTypes.Audio, SaveTypes.SoundEffectVolume, currentSettingsModel.sfxVolume.ToString());
            }
        }
        #endregion

        #region Gameplay Settings
        public float MouseSensitivity {
            get { return currentSettingsModel.mouseSensitivity; }
            set {
                currentSettingsModel.mouseSensitivity = value;
                SaveController.Instance.WriteAndSaveData(SaveTypes.Gameplay, SaveTypes.MouseSensitivity, currentSettingsModel.mouseSensitivity.ToString());
            }
        }

        public float ControllerSensitivity {
            get { return currentSettingsModel.controllerSensitivity; }
            set {
                currentSettingsModel.controllerSensitivity = value;
                SaveController.Instance.WriteAndSaveData(SaveTypes.Gameplay, SaveTypes.ControllerSensitivity, currentSettingsModel.controllerSensitivity.ToString());
            }
        }

        public bool MouseYInverted {
            get { return currentSettingsModel.mouseYInverted; }
            set {
                currentSettingsModel.mouseYInverted = value;
                SaveController.Instance.WriteAndSaveData(SaveTypes.Gameplay, SaveTypes.MouseYInverted, currentSettingsModel.mouseYInverted.ToString());
            }
        }

        public bool ControllerYInverted {
            get { return currentSettingsModel.controllerYInverted; }
            set {
                currentSettingsModel.controllerYInverted = value;
                SaveController.Instance.WriteAndSaveData(SaveTypes.Gameplay, SaveTypes.ControllerYInverted, currentSettingsModel.controllerYInverted.ToString());
            }
        }

        public bool MouseSmoothing {
            get { return currentSettingsModel.mouseSmoothing; }
            set {
                currentSettingsModel.mouseSmoothing = value;
                SaveController.Instance.WriteAndSaveData(SaveTypes.Gameplay, SaveTypes.MouseSmoothing, currentSettingsModel.mouseSmoothing.ToString());
            }
        }

        public bool ToggleCrouch {
            get { return currentSettingsModel.toggleCrouch; }
            set {
                currentSettingsModel.toggleCrouch = value;
                SaveController.Instance.WriteAndSaveData(SaveTypes.Gameplay, SaveTypes.ToggleCrouch, currentSettingsModel.toggleCrouch.ToString());
            }
        }
        #endregion

        private void LoadDefaultGraphicsSettings(object[] arg0) {
            Resolution = new Vector2(Screen.currentResolution.width, Screen.currentResolution.height);
            AspectRatio = CustomSerializableObjects.Vector2FromString(defaultSettingsModel.aspectRatio);
            Fullscreen = defaultSettingsModel.fullscreen;
            VSync = defaultSettingsModel.vSync;
            Preset = defaultSettingsModel.preset;
            TextureQuality = defaultSettingsModel.textureQuality;
            ShadowQuality = defaultSettingsModel.shadowQuality;
            AA = defaultSettingsModel.AA;
            FOV = defaultSettingsModel.FOV;
        }

        private void LoadDefaultAudioSettings(object[] arg0) {
            MusicVolume = defaultSettingsModel.musicVolume;
            SoundEffectVolume = defaultSettingsModel.sfxVolume;
        }

        private void LoadDefaultGameplaySettings(object[] arg0) {
            MouseSensitivity = defaultSettingsModel.mouseSensitivity;
            ControllerSensitivity = defaultSettingsModel.controllerSensitivity;
            MouseYInverted = defaultSettingsModel.mouseYInverted;
            ControllerYInverted = defaultSettingsModel.controllerYInverted;
            MouseSmoothing = defaultSettingsModel.mouseSmoothing;
            ToggleCrouch = defaultSettingsModel.toggleCrouch;

            EventManager.TriggerEvent(InputEventTypes.ON_GAMEPAD_SENSITIVITY_CHANGED, ControllerSensitivity);
            EventManager.TriggerEvent(InputEventTypes.ON_GAMEPAD_Y_CHANGED, ControllerYInverted);
            EventManager.TriggerEvent(InputEventTypes.ON_MOUSE_SENSITIVITY_CHANGED, MouseSensitivity);
            EventManager.TriggerEvent(InputEventTypes.ON_MOUSE_Y_CHANGED, MouseYInverted);
        }

        private void LoadDefaultSettings(object[] arg0) {
            //Ignore EventManager, save cpu time.
            LoadDefaultGraphicsSettings(null);
            LoadDefaultAudioSettings(null);
            LoadDefaultGameplaySettings(null);
            LoadDataFromSaveFile(null);
        }

        private void LoadDataFromSaveFile(object[] arg0) {
            //Graphics Settings
            resolution = CustomSerializableObjects.Vector2FromString(SaveController.Instance.ReadData(SaveTypes.Graphics, SaveTypes.Resolution));
            currentSettingsModel.aspectRatio = CustomSerializableObjects.Vector2FromString(SaveController.Instance.ReadData(SaveTypes.Graphics, SaveTypes.AspectRatio)).ToString();
            currentSettingsModel.fullscreen = CustomSerializableObjects.ToBoolean(SaveController.Instance.ReadData(SaveTypes.Graphics, SaveTypes.Fullscreen));
            currentSettingsModel.vSync = CustomSerializableObjects.ToBoolean(SaveController.Instance.ReadData(SaveTypes.Graphics, SaveTypes.Vsync));
            currentSettingsModel.preset = Int32.Parse(SaveController.Instance.ReadData(SaveTypes.Graphics, SaveTypes.Preset));
            currentSettingsModel.textureQuality = Int32.Parse(SaveController.Instance.ReadData(SaveTypes.Graphics, SaveTypes.TextureQuality));
            currentSettingsModel.shadowQuality = Int32.Parse(SaveController.Instance.ReadData(SaveTypes.Graphics, SaveTypes.ShadowQuality));
            currentSettingsModel.AA = Int32.Parse(SaveController.Instance.ReadData(SaveTypes.Graphics, SaveTypes.AntiAliasing));
            currentSettingsModel.anisotropicFiltering = CustomSerializableObjects.ToBoolean(SaveController.Instance.ReadData(SaveTypes.Graphics, SaveTypes.AnisotropicFiltering));
            currentSettingsModel.FOV = int.Parse(SaveController.Instance.ReadData(SaveTypes.Graphics, SaveTypes.FieldOfView));

            //Audio Settings
            currentSettingsModel.musicVolume = float.Parse(SaveController.Instance.ReadData(SaveTypes.Audio, SaveTypes.MusicVolume));
            currentSettingsModel.sfxVolume = float.Parse(SaveController.Instance.ReadData(SaveTypes.Audio, SaveTypes.SoundEffectVolume));

            //Gameplay Settings
            currentSettingsModel.mouseSensitivity = float.Parse(SaveController.Instance.ReadData(SaveTypes.Gameplay, SaveTypes.MouseSensitivity));
            currentSettingsModel.controllerSensitivity = float.Parse(SaveController.Instance.ReadData(SaveTypes.Gameplay, SaveTypes.ControllerSensitivity));
            currentSettingsModel.mouseYInverted = CustomSerializableObjects.ToBoolean(SaveController.Instance.ReadData(SaveTypes.Gameplay, SaveTypes.MouseYInverted));
            currentSettingsModel.controllerYInverted = CustomSerializableObjects.ToBoolean(SaveController.Instance.ReadData(SaveTypes.Gameplay, SaveTypes.ControllerYInverted));
            currentSettingsModel.mouseSmoothing = CustomSerializableObjects.ToBoolean(SaveController.Instance.ReadData(SaveTypes.Gameplay, SaveTypes.MouseSmoothing));
            currentSettingsModel.toggleCrouch = CustomSerializableObjects.ToBoolean(SaveController.Instance.ReadData(SaveTypes.Gameplay, SaveTypes.ToggleCrouch));

            EventManager.TriggerEvent(GameDataEventTypes.GAME_DATA_READY);
        }
    }
}
