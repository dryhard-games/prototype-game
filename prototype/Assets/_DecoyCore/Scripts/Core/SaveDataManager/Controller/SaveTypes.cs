namespace Decoy.Core.SaveSystem {
    /// <summary>
    /// SaveTypes.cs
    /// <summary>
    /// Author: Thomas van Opstal
    /// <summary>
    /// 
    /// </summary>
    public class SaveTypes {
        public const string ROOT = "Settings";

        #region Categories
        public const string Graphics = "Graphics";
        public const string Audio = "Audio";
        public const string Gameplay = "Gameplay";
        public const string GameSettings = "GameSettings";
        public const string Keybindings = "Keybindings";
        #endregion

        #region Graphics
        public const string Resolution = "Resolution";
        public const string AspectRatio = "AspectRatio";
        public const string Fullscreen = "Fullscreen";
        public const string Vsync = "Vsync";
        public const string RefreshRate = "RefreshRate";
        public const string Preset = "Preset";
        public const string TextureQuality = "TextureQuality";
        public const string ShadowQuality = "ShadowQuality";
        public const string AntiAliasing = "AntiAliasing";
        public const string AnisotropicFiltering = "AnisotropicFiltering";
        public const string FieldOfView = "FieldOfView";
        #endregion

        #region Audio
        public const string MusicVolume = "MusicVolume";
        public const string SoundEffectVolume = "SoundEffectVolume";
        #endregion

        #region Gameplay
        public const string MouseSensitivity = "MouseSensitivity";
        public const string MouseYInverted = "MouseYInverted";
        public const string ControllerSensitivity = "ControllerSensitivity";
        public const string ControllerYInverted = "ControllerYInverted";
        public const string ToggleCrouch = "ToggleCrouch";
        public const string MouseSmoothing = "MouseSmoothing";
        #endregion

        #region GameSettings
        public const string SaveGameVersion = "SaveGameVersion";
        public const string FirstRun = "FirstRun";
        #endregion
    }
}
