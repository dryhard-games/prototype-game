namespace Decoy.Core.Message {
    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.EventSystems;
    using System.Collections;
    /// <summary>
    /// MessagePanelView.cs
    /// <summary>
    /// Author: Thomas van Opstal
    /// </summary>
    public class MessagePanelView : MonoBehaviour {
        public Text messageTypeText;
        public Text messageText;
        public Button dismissButton;

        private string messageType;
        private string message;
        private EventSystem eventSystem;

        public string MessageType {
            get { return messageType; }
            set {
                messageType = value;
                messageTypeText.text = messageType;
            }
        }

        public string Message {
            get { return message; }
            set {
                message = value;
                messageText.text = message;
            }
        }

        private void OnEnable() {
            eventSystem = FindObjectOfType<EventSystem>();
            dismissButton.onClick.AddListener(DismissPanel);

            if (eventSystem != null)
                StartCoroutine(SetupEventSystem());
        }

        private IEnumerator SetupEventSystem() {
            eventSystem.SetSelectedGameObject(null);
            yield return new WaitForSeconds(0.5f);
            eventSystem.SetSelectedGameObject(dismissButton.gameObject);
        }

        private void DismissPanel() {
            Destroy(gameObject);
        }
    }
}
