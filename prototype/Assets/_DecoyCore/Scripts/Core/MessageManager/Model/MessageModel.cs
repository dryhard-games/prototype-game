namespace Decoy.Core.Message {
    using System;
    using UnityEngine;
    /// <summary>
    /// MessageModel.cs
    /// <summary>
    /// Author: Thomas van Opstal
    /// </summary>
    [CreateAssetMenu(fileName = "MessageModel", menuName = "Decoy/Messages/New Message Model")]
    public class MessageModel : ScriptableObject {
        [Serializable]
        public struct MessageDic {
            public Messages key;
            public string value;
        }

        public MessageDic[] messages;
    }
}
