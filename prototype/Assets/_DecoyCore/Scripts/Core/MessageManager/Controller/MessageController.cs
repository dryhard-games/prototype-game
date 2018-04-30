namespace Decoy.Core.Message {
    using UnityEngine;
    using Decoy.Core.EventSystem;
    using System.Collections.Generic;
    /// <summary>
    /// MessageController.cs
    /// <summary>
    /// Author: Thomas van Opstal
    /// </summary>
    public class MessageController : MonoBehaviour {
        public MessageModel messageModel;

        public GameObject messageCanvas;
        public GameObject messagePanelPrefab;

        private MessageModel.MessageDic[] messageDictionary;
        private Dictionary<Messages, string> lookUpDictionary = new Dictionary<Messages, string>();

        private void Start() {
            EventManager.StartListening(MessageEventTypes.DISPLAY_MESSAGE, DisplayMessage);

            if (messageModel == null)
                return;

            messageDictionary = messageModel.messages;

            foreach (MessageModel.MessageDic entry in messageDictionary)
                lookUpDictionary.Add(entry.key, entry.value);
        }

        private void OnDestroy() {
            EventManager.StopListening(MessageEventTypes.DISPLAY_MESSAGE, DisplayMessage);
        }

        private void DisplayMessage(object[] data) {
            MessageTypes messageType = (MessageTypes)data[0];
            Messages message = (Messages)data[1];

            GameObject newMessagePanel = Instantiate(messagePanelPrefab, messageCanvas.transform);
            MessagePanelView panelView = newMessagePanel.GetComponent<MessagePanelView>();

            panelView.MessageType = messageType.ToString();
            panelView.Message = lookUpDictionary[message];
        }
    }
}
