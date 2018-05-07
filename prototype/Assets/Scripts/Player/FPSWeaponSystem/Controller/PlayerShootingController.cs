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
        public Camera playerViewport;

        private void Update() {
            if (!inputModel.detectInput)
                return;

            AimTowardsCenter();

            if (inputModel.interactButtonDown)
                EventManager.TriggerEvent(GunEventTypes.FIRE_PROJECTILE, ProjectileTypes.Bullet, weaponMuzzle.position.ToString("G4"), weaponMuzzle.eulerAngles.ToString("G4"));
        }

        //gun muzzleExit always pointed toward the object in the center
        private void AimTowardsCenter() {
            float screenX = Screen.width / 2;
            float screenY = Screen.height / 2;

            RaycastHit hit;
            Ray ray = playerViewport.ScreenPointToRay(new Vector3(screenX, screenY));

            if (Physics.Raycast(ray, out hit))
                weaponMuzzle.LookAt(hit.point);

        }
    }
}
