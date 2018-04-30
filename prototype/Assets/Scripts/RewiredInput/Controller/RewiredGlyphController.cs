namespace Decoy.Feature.RewiredInput {
    using UnityEngine;
    using Rewired;
    /// <summary>
    /// RewiredGlyphController.cs
    /// <summary>
    /// Author: Thomas van Opstal
    /// </summary>
    [CreateAssetMenu(fileName = "RewiredGlyphController", menuName = "Decoy/Input/Rewired/New Rewired Glyph Controller")]
    public class RewiredGlyphController : ScriptableObject {
        public RewiredGamepadModel[] gamepadModels;

        public Sprite GetGlyph(string joystickGuid, int elementIdentifierId, AxisRange axisRange) {
            for (int i = 0; i < gamepadModels.Length; i++) {
                if (gamepadModels[i].joystickGUID == joystickGuid)
                    return GetRequestedGlyph(elementIdentifierId, axisRange, i);
            }

            return null;
        }

        private Sprite GetRequestedGlyph(int elementIdentifierId, AxisRange axisRange, int modelIndex) {
            for (int i = 0; i < gamepadModels[modelIndex].gamepadGlyphModel.Length; i++) {
                if (gamepadModels[modelIndex].gamepadGlyphModel[i].elementIdentifierId == elementIdentifierId) {
                    switch (axisRange) {
                        case AxisRange.Full:
                            return gamepadModels[modelIndex].gamepadGlyphModel[i].glyph;
                        case AxisRange.Negative:
                            return gamepadModels[modelIndex].gamepadGlyphModel[i].glyphNeg;
                        case AxisRange.Positive:
                            return gamepadModels[modelIndex].gamepadGlyphModel[i].glyphPos;
                    }
                }
            }

            return null;
        }
    }
}
