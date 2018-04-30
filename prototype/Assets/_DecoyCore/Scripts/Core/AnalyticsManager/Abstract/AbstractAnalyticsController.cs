namespace Decoy.Core.Analytics {
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    /// <summary>
    /// AbstractAnalyticsController.cs
    /// <summary>
    /// Author: Thomas van Opstal
    /// </summary>
    [Serializable]
    public abstract class AbstractAnalyticsController : ScriptableObject {
        public abstract void SendAnalyticEvent(string eventType, Dictionary<string, object> data);
    }
}
