namespace Decoy.Core.Analytics {
    /// <summary>
    /// AnalyticsEventTypes.cs
    /// <summary>
    /// Author: Thomas van Opstal
    /// </summary>
    public class AnalyticsEventTypes {
        private const string PREFIX = "ANALYTICS_";

        /// <summary>
        /// Arguments - 1. (string) eventName 2. (Dictionary string, object) event data
        /// </summary>
        public const string SEND_ANALYTIC_EVENT = PREFIX + "SEND_ANALYTIC_EVENT";
    }
}
