namespace Dryhard.Player.Weapon {
    using UnityEngine;
    /// <summary>
    /// WeaponView.cs
    /// <summary>
    /// Author: Thomas van Opstal
    /// </summary>
    public class WeaponView : MonoBehaviour {
        public Transform weaponAnchor;
        public Transform cameraAnchor;
        public float lerpSpeed;

        private void Update() {
            weaponAnchor.rotation = Quaternion.Lerp(weaponAnchor.rotation, cameraAnchor.rotation, Time.deltaTime * lerpSpeed);
        }
    }
}
