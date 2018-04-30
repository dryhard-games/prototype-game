namespace Decoy.Feature.Achievements {
    using UnityEngine;
    using Steamworks;
    using Decoy.Core.Achievements;
    /// <summary>
    /// SteamAchievementProcessor.cs
    /// <summary>
    /// Author: Thomas van Opstal
    /// </summary>
    [CreateAssetMenu(fileName = "SteamAchievementProcessor", menuName = "Decoy/Achievement/Steam Achievement Processor", order = 1)]
    public class SteamAchievementProcessor : AbstractAchievementController {
        public override void UnlockAchievement(string achievementID) {
            if (SteamManager.Initialized) {
                bool AchievementUnlocked;
                SteamUserStats.GetAchievement(achievementID, out AchievementUnlocked);

                if (!AchievementUnlocked) {
                    SteamUserStats.SetAchievement(achievementID);
                    SteamUserStats.StoreStats();
                }
            }
        }
    }
}
