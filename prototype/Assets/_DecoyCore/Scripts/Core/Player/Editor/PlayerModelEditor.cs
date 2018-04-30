namespace Decoy.Core.Player {
    using UnityEngine;
    using UnityEditor;
    /// <summary>
    /// PlayerModelEditor.cs
    /// <summary>
    /// Author: Thomas van Opstal
    /// </summary>
    [CustomEditor(typeof(PlayerModel))]
    public class PlayerModelEditor : Editor {
        private PlayerModel model;

        public override void OnInspectorGUI() {
            model = (PlayerModel)target;

            DrawDefaultInspector();

            GUILayout.Space(10.0f);

            if (GUILayout.Button("Save Settings", GUILayout.MinWidth(200), GUILayout.MaxWidth(200))) {
                EditorUtility.SetDirty(model);
                AssetDatabase.SaveAssets();
            }
        }
    }
}
