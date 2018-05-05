namespace Dryhard.Player.Weapon {
    using UnityEngine;
    using Decoy.Core.EventSystem;
    using Decoy.Core.InputSystem;
    using Dryhard.GunSystem;
    /// <summary>
    /// PlayerShootingController.cs
    /// <summary>
    /// Author: Thomas van Opstal
    /// </summary>
    public class PlayerShootingController : MonoBehaviour {
        public Transform weaponMuzzle;
        public InputModel inputModel;

        private void Update() {
            if (!inputModel.detectInput)
                return;

            if (inputModel.interactButtonDown)
                EventManager.TriggerEvent(GunEventTypes.FIRE_PROJECTILE, ProjectileTypes.Bullet, weaponMuzzle.position.ToString("G4"), weaponMuzzle.eulerAngles.ToString("G4"));
        }
    }
}
