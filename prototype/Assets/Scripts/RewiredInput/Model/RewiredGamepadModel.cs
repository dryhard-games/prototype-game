namespace Decoy.Feature.RewiredInput {
    using UnityEngine;
    using System;
    /// <summary>
    /// RewiredGamepadModel.cs
    /// <summary>
    /// Author: Thomas van Opstal
    /// <summary>
    [CreateAssetMenu(fileName = "RewiredGamepadModel", menuName = "Decoy/Input/Rewired/New Rewired Gamepad Model")]
    public class RewiredGamepadModel : ScriptableObject {
        [Serializable]
        public struct GlyphEntry {
            public string buttonName;
            public int elementIdentifierId;
            public Sprite glyph;
            public Sprite glyphPos;
            public Sprite glyphNeg;
        }

        public string controllerName;
        public string joystickGUID;
        public GlyphEntry[] gamepadGlyphModel;
    }
}
