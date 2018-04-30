namespace Decoy.Core.Message {
    using UnityEngine;
    using UnityEditor;
    using System;
    using System.Collections.Generic;
    /// <summary>
    /// MessageModelEditor.cs
    /// <summary>
    /// Author: Thomas van Opstal
    /// </summary>
    [CustomEditor(typeof(MessageModel))]
    public class MessageModelEditor : Editor {
        public override void OnInspectorGUI() {
            MessageModel model = (MessageModel)target;
            if (model.messages == null) {
                model.messages = new MessageModel.MessageDic[Enum.GetNames(typeof(Messages)).Length];

                int i = 0;
                foreach (Messages type in Enum.GetValues(typeof(Messages))) {
                    model.messages[i].key = type;
                    model.messages[i].value = null;
                    i++;
                }
            }

            if (model.messages.Length != Enum.GetNames(typeof(Messages)).Length) {
                List<MessageModel.MessageDic> oldData = new List<MessageModel.MessageDic>();

                for (int i = 0; i < model.messages.Length; i++)
                    oldData.Add(model.messages[i]);

                List<MessageModel.MessageDic> newData = new List<MessageModel.MessageDic>();

                newData = oldData;

                //Remove old enums that are being used anymore
                foreach (MessageModel.MessageDic dic in newData.ToArray()) {
                    bool found = false;

                    foreach (Messages type in Enum.GetValues(typeof(Messages))) {
                        if (dic.key == type)
                            found = true;
                    }

                    if (!found)
                        newData.Remove(dic);
                }

                //Check for new Enums
                foreach (Messages type in Enum.GetValues(typeof(Messages))) {
                    bool found = false;

                    foreach (MessageModel.MessageDic dic in newData)
                        if (type == dic.key)
                            found = true;

                    if (!found)
                        newData.Add(new MessageModel.MessageDic() { key = type, value = null });
                }

                model.messages = new MessageModel.MessageDic[Enum.GetNames(typeof(Messages)).Length];

                for (int i = 0; i < model.messages.Length; i++)
                    model.messages[i] = newData[i];
            }

            for (int i = 0; i < model.messages.Length; i++) {
                EditorGUILayout.BeginHorizontal();
                GUILayout.Label(model.messages[i].key.ToString(), EditorStyles.boldLabel, GUILayout.MinWidth(100), GUILayout.MaxWidth(200));
                model.messages[i].value = EditorGUILayout.TextField(model.messages[i].value, GUILayout.MinWidth(100), GUILayout.MaxWidth(200));
                EditorGUILayout.EndHorizontal();
            }

            if (GUILayout.Button("Save")) {
                EditorUtility.SetDirty(model);
                AssetDatabase.SaveAssets();
            }
        }
    }
}
