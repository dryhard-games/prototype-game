namespace Decoy.Feature.RewiredInput {
    using UnityEngine;
    using UnityEngine.UI;
    using Rewired;
    /// <summary>
    /// RewiredRemapModel.cs
    /// <summary>
    /// Author: Thomas van Opstal
    /// </summary>
    [CreateAssetMenu(fileName = "RewiredRemapModel", menuName = "Decoy/Input/Rewired/New Rewired Remap Model")]
    public class RewiredRemapModel : ScriptableObject {
        [Header("InputMapper Settings")]
        public float timeOut;
        public bool ignoreMouseYAxis;
        public bool ignoreMouseXAxis;
        public bool checkForConflicts;
        public InputMapper.ConflictResponse conflictResponse;

        [Header("UI Elements - Prefabs Only")]
        public GameObject buttonPrefab;
        public GameObject textPrefab;

        [Header("Exceptions")]
        public ACTIONS[] keyboardExceptions;
    }

    public class Row {
        public InputAction action;
        public AxisRange actionRange;
        public Button button;
        public Text text;
        public Image glyph;
    }
}
