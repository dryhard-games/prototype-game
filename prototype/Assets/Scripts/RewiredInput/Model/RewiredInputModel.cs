namespace Decoy.Feature.RewiredInput {
    using UnityEngine;
    using Rewired;
    /// <summary>
    /// RewiredInputModel.cs
    /// <summary>
    /// Author: Thomas van Opstal
    /// </summary>
    [CreateAssetMenu(fileName = "RewiredInputModel", menuName = "Decoy/Input/Rewired/New Rewired Input Model")]
    public class RewiredInputModel : ScriptableObject {
        public float mappingTimeout;
        public bool ignoreMouseXAxis;
        public bool ignoreMouseYAxis;
        public bool checkForConflicts;
        public InputMapper.ConflictResponse conflictResponse;
    }

    public enum ACTIONS {
        HorizontalMovement = 0,
        VerticalMovement = 1,
        Jump = 2,
        Interact = 3,
        Crouch = 4,
        PushToTalk = 5,
        HorizontalLook = 6,
        VerticalLook = 7,
        Sprint = 8
    }
}
