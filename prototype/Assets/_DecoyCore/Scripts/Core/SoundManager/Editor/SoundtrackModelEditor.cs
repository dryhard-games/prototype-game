namespace Decoy.Core.SoundSystem {
    using UnityEngine;
    using UnityEditor;
    using System;
    using System.Collections.Generic;
    /// <summary>
    /// SoundtrackModelEditor.cs
    /// <summary>
    /// Author: Thomas van Opstal
    /// </summary>
    [CustomEditor(typeof(SoundtrackModel))]
    public class SoundtrackModelEditor : Editor {
        public override void OnInspectorGUI() {
            SoundtrackModel soundtrackModel = (SoundtrackModel)target;

            if (soundtrackModel.soundtracks == null) {
                soundtrackModel.soundtracks = new SoundtrackModel.SoundDictionary[Enum.GetNames(typeof(SOUNDTRACK_TYPES)).Length];

                int i = 0;
                foreach (SOUNDTRACK_TYPES type in Enum.GetValues(typeof(SOUNDTRACK_TYPES))) {
                    soundtrackModel.soundtracks[i].key = type;
                    soundtrackModel.soundtracks[i].value = null;
                    i++;
                }
            }

            if (soundtrackModel.soundtracks.Length != Enum.GetNames(typeof(SOUNDTRACK_TYPES)).Length) {
                List<SoundtrackModel.SoundDictionary> oldData = new List<SoundtrackModel.SoundDictionary>();

                for (int i = 0; i < soundtrackModel.soundtracks.Length; i++) {
                    oldData.Add(soundtrackModel.soundtracks[i]);
                }

                List<SoundtrackModel.SoundDictionary> newData = new List<SoundtrackModel.SoundDictionary>();

                newData = oldData;

                //Remove old enums that are being used anymore
                foreach (SoundtrackModel.SoundDictionary dic in newData.ToArray()) {
                    bool found = false;

                    foreach (SOUNDTRACK_TYPES type in Enum.GetValues(typeof(SOUNDTRACK_TYPES))) {
                        if (dic.key == type)
                            found = true;
                    }

                    if (!found)
                        newData.Remove(dic);
                }

                //Check for new Enums
                foreach (SOUNDTRACK_TYPES type in Enum.GetValues(typeof(SOUNDTRACK_TYPES))) {
                    bool found = false;

                    foreach (SoundtrackModel.SoundDictionary dic in newData) {
                        if (type == dic.key)
                            found = true;
                    }

                    if (!found)
                        newData.Add(new SoundtrackModel.SoundDictionary() { key = type, value = null });
                }

                soundtrackModel.soundtracks = new SoundtrackModel.SoundDictionary[Enum.GetNames(typeof(SOUNDTRACK_TYPES)).Length];

                for (int i = 0; i < soundtrackModel.soundtracks.Length; i++) {
                    soundtrackModel.soundtracks[i] = newData[i];
                }
            }

            for (int i = 0; i < soundtrackModel.soundtracks.Length; i++) {
                EditorGUILayout.BeginHorizontal();
                GUILayout.Label(soundtrackModel.soundtracks[i].key.ToString(), EditorStyles.boldLabel, GUILayout.MinWidth(100), GUILayout.MaxWidth(200));
                soundtrackModel.soundtracks[i].value = (AudioClip)EditorGUILayout.ObjectField(soundtrackModel.soundtracks[i].value, typeof(AudioClip), false, GUILayout.MinWidth(100), GUILayout.MaxWidth(200));
                EditorGUILayout.EndHorizontal();
            }

            if (GUILayout.Button("Save")) {
                EditorUtility.SetDirty(soundtrackModel);
                AssetDatabase.SaveAssets();
            }
        }
    }
}
