namespace Decoy.Core.Analytics {
    using UnityEngine;
    using Decoy.Core.EventSystem;
    using System.Collections.Generic;
    /// <summary>
    /// AnalysticsController.cs
    /// <summary>
    /// Author: Thomas van Opstal
    /// </summary>
    public class AnalysticsController : MonoBehaviour {
        public AbstractAnalyticsController analyticsProcessor;

        private void Start() {
            EventManager.StartListening(AnalyticsEventTypes.SEND_ANALYTIC_EVENT, SendAnalyticEvent);
        }

        private void OnDestroy() {
            EventManager.StopListening(AnalyticsEventTypes.SEND_ANALYTIC_EVENT, SendAnalyticEvent);
        }

        private void SendAnalyticEvent(object[] data) {
            analyticsProcessor.SendAnalyticEvent((string)data[0], (Dictionary<string, object>)data[0]);
        }
    }
}
