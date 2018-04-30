namespace Decoy.Feature.Analytics {
    using Decoy.Core.Analytics;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Analytics;
    /// <summary>
    /// UnityAnalyticsProcessor.cs
    /// <summary>
    /// Author: Thomas van Opstal
    /// </summary>
    [CreateAssetMenu(fileName = "UnityAnalyticsProcessor", menuName = "Decoy/Analytics/Unity Analytics Processor")]
    public class UnityAnalyticsProcessor : AbstractAnalyticsController {
        public override void SendAnalyticEvent(string eventType, Dictionary<string, object> data) {
            Analytics.CustomEvent(eventType, data);
        }
    }
}
