namespace Decoy.Feature.RewiredInput {
    using UnityEngine;
    using UnityEditor;
    using System.Collections.Generic;
    /// <summary>
    /// RewiredGamepadModelEditor.cs
    /// <summary>
    /// Author: Thomas van Opstal
    /// </summary>
    [CustomEditor(typeof(RewiredGamepadModel))]
    public class RewiredGamepadModelEditor : Editor {
        private RewiredGamepadModel model;

        public override void OnInspectorGUI() {
            model = (RewiredGamepadModel)target;
            EditorGUILayout.LabelField("Gamepad Configuration", EditorStyles.boldLabel, GUILayout.MaxWidth(200));
            GUILayout.Space(5);

            GUILayout.BeginHorizontal();
            GUILayout.Label("Gamepad Name: ", EditorStyles.label, GUILayout.MinWidth(200), GUILayout.MaxWidth(200));
            model.controllerName = EditorGUILayout.TextField(model.controllerName, GUILayout.MinWidth(200), GUILayout.MaxWidth(200));
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("JoystickGUID: ", EditorStyles.label, GUILayout.MinWidth(200), GUILayout.MaxWidth(200));
            model.joystickGUID = EditorGUILayout.TextField(model.joystickGUID, GUILayout.MinWidth(200), GUILayout.MaxWidth(200));
            GUILayout.EndHorizontal();

            DrawGlyphModel();

            if (GUILayout.Button("Save Settings", GUILayout.MinWidth(200), GUILayout.MaxWidth(200))) {
                EditorUtility.SetDirty(model);
                AssetDatabase.SaveAssets();
            }
        }

        private void DrawGlyphModel() {
            List<RewiredGamepadModel.GlyphEntry> glyphModels = new List<RewiredGamepadModel.GlyphEntry>();

            if (model.gamepadGlyphModel != null) {
                foreach (RewiredGamepadModel.GlyphEntry model in model.gamepadGlyphModel)
                    glyphModels.Add(model);
            }

            if (GUILayout.Button("Add Glyph", GUILayout.MinWidth(200), GUILayout.MaxWidth(200)))
                glyphModels.Add(new RewiredGamepadModel.GlyphEntry());

            model.gamepadGlyphModel = glyphModels.ToArray();

            for (int j = 0; j < model.gamepadGlyphModel.Length; j++) {
                GUILayout.BeginHorizontal();
                GUILayout.Label("Element Name: ", EditorStyles.label, GUILayout.MinWidth(200), GUILayout.MaxWidth(200));
                model.gamepadGlyphModel[j].buttonName = EditorGUILayout.TextField(model.gamepadGlyphModel[j].buttonName, GUILayout.MinWidth(200), GUILayout.MaxWidth(200));
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Label("Element Identifier ID: ", EditorStyles.label, GUILayout.MinWidth(200), GUILayout.MaxWidth(200));
                model.gamepadGlyphModel[j].elementIdentifierId = EditorGUILayout.IntField(model.gamepadGlyphModel[j].elementIdentifierId, GUILayout.MinWidth(200), GUILayout.MaxWidth(200));
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Label("Glyph: ", EditorStyles.label, GUILayout.MinWidth(200), GUILayout.MaxWidth(200));
                model.gamepadGlyphModel[j].glyph = (Sprite)EditorGUILayout.ObjectField(model.gamepadGlyphModel[j].glyph, typeof(Sprite), false, GUILayout.MinWidth(200), GUILayout.MaxWidth(200));
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Label("Glyph Neg: ", EditorStyles.label, GUILayout.MinWidth(200), GUILayout.MaxWidth(200));
                model.gamepadGlyphModel[j].glyphNeg = (Sprite)EditorGUILayout.ObjectField(model.gamepadGlyphModel[j].glyphNeg, typeof(Sprite), false, GUILayout.MinWidth(200), GUILayout.MaxWidth(200));
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Label("Glyph Pos: ", EditorStyles.label, GUILayout.MinWidth(200), GUILayout.MaxWidth(200));
                model.gamepadGlyphModel[j].glyphPos = (Sprite)EditorGUILayout.ObjectField(model.gamepadGlyphModel[j].glyphPos, typeof(Sprite), false, GUILayout.MinWidth(200), GUILayout.MaxWidth(200));
                GUILayout.EndHorizontal();

                if (GUILayout.Button("Remove Glyph Entry", GUILayout.MinWidth(200), GUILayout.MaxWidth(200)))
                    RemoveRequestedGlyphModel(j);

                GUILayout.Space(5.0f);
            }
        }

        private void RemoveRequestedGlyphModel(int index) {
            List<RewiredGamepadModel.GlyphEntry> glyphModels = new List<RewiredGamepadModel.GlyphEntry>();

            if (model.gamepadGlyphModel != null) {
                foreach (RewiredGamepadModel.GlyphEntry model in model.gamepadGlyphModel)
                    glyphModels.Add(model);
            }

            glyphModels.RemoveAt(index);

            model.gamepadGlyphModel = glyphModels.ToArray();
        }
    }
}
