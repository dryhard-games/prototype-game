namespace Decoy.Core.Engagementscreen {
    using UnityEngine;
    using UnityEngine.UI;
    using Decoy.Core.EventSystem;
    /// <summary>
    /// EngagementScreenView.cs
    /// <summary>
    /// Author: Thomas van Opstal
    /// <summary>
    /// 
    /// </summary>
    public class EngagementScreenView : MonoBehaviour {
        public Text statusText;

        private void Start() {
            EventManager.StartListening(EngagementEventTypes.CHANGE_STATUS_TEXT, UpdateStatusText);

            statusText.text = EngagementScreenStatusTypes.PRESS_ANY_BUTTON;
        }

        private void OnDestroy() {
            EventManager.StopListening(EngagementEventTypes.CHANGE_STATUS_TEXT, UpdateStatusText);
        }

        private void UpdateStatusText(object[] data) {
            if ((string)data[0] == string.Empty)
                statusText.text = "";
            else
                statusText.text = (string)data[0];
        }
    }
}
