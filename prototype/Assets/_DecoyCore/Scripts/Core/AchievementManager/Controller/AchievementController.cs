namespace Decoy.Core.Achievements {
    using UnityEngine;
    using Decoy.Core.EventSystem;
    /// <summary>
    /// AchievementController.cs
    /// <summary>
    /// Author: Thomas van Opstal
    /// </summary>
    public class AchievementController : MonoBehaviour {
        public AbstractAchievementController achievementProcessor;

        private void Start() {
            EventManager.StartListening(AchievementEventTypes.UNLOCK_ACHIEVEMENT, UnlockAchievement);
        }

        private void OnDestroy() {
            EventManager.StopListening(AchievementEventTypes.UNLOCK_ACHIEVEMENT, UnlockAchievement);
        }

        private void UnlockAchievement(object[] data) {
            achievementProcessor.UnlockAchievement((string)data[0]);
        }
    }
}
