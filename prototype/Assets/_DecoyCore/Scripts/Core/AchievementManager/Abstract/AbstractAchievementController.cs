namespace Decoy.Core.Achievements {
    using UnityEngine;
    using System;
    /// <summary>
    /// AbstractAchievementController.cs
    /// <summary>
    /// Author: Thomas van Opstal
    /// </summary>
    [Serializable]
    public abstract class AbstractAchievementController : ScriptableObject {
        public abstract void UnlockAchievement(string achievementID);
    }
}
