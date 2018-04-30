namespace Decoy.Core.SoundSystem {
    using System;
    using UnityEngine;
    /// <summary>
    /// SoundEventTypes.cs
    /// <summary>
    /// Author: Thomas van Opstal
    /// </summary>
    [CreateAssetMenu(fileName = "SoundtrackModelData", menuName = "Decoy/Sound/SoundtrackModel")]
    public class SoundtrackModel : ScriptableObject {
        [Serializable]
        public struct SoundDictionary {
            public SOUNDTRACK_TYPES key;
            public AudioClip value;
        }

        public SoundDictionary[] soundtracks;
    }
}
