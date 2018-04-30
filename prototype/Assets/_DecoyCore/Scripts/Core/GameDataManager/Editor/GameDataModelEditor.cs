namespace Decoy.Core.GameDataSystem {
    using UnityEngine;
    using UnityEditor;
    /// <summary>
    /// GameDataModelEditor.cs
    /// <summary>
    /// Author: Thomas van Opstal
    /// </summary>
    [CustomEditor(typeof(GameDataModel))]
    public class GameDataModelEditor : Editor {
        private GameDataModel gameDataModel;

        public override void OnInspectorGUI() {
            gameDataModel = (GameDataModel)target;
            DrawDefaultInspector();

            if (GUILayout.Button("Save Settings")) {
                EditorUtility.SetDirty(gameDataModel);
                AssetDatabase.SaveAssets();
            }
        }
    }
}
