namespace Dryhard.Player.GunSystem {
    using UnityEngine;
    using Decoy.Core.InputSystem;
    using Decoy.Core.EventSystem;
    using System.Collections;
    using Dryhard.GunSystem;
    /// <summary>
    /// PlayerGunController.cs
    /// <summary>
    /// Author: Thomas van Opstal
    /// </summary>
    public class PlayerGunController : MonoBehaviour {
        public InputModel iModel;
        public GunModel gModel;
        public Camera playerCamera;
        public Transform muzzleExit;

        private bool isReloading;
        private float lastShot;

        private int roundsLeft;

        private void Start() {
            EventManager.StartListening(PlayerGunEventTypes.WEAPON_SWITCH_REQUEST, SwitchWeapon);
        }

        private void OnDestroy() {
            EventManager.StopListening(PlayerGunEventTypes.WEAPON_SWITCH_REQUEST, SwitchWeapon);
        }

        private void Update() {
            if (isReloading)
                return;

            AimAtCenter();

            if (roundsLeft <= 0) {
                StartCoroutine(Reload());
                return;
            }

            if (iModel.interactButtonHold)
                Fire();
        }

        private IEnumerator Reload() {
            isReloading = true;
            yield return new WaitForSeconds(gModel.reloadSpeed);
            roundsLeft = gModel.magazineSize;
            isReloading = false;
        }

        private void Fire() {
            if (Time.time > gModel.rateOfFire + lastShot) {
                roundsLeft--;

                EventManager.TriggerEvent(GunEventTypes.FIRE_PROJECTILE, gModel.bulletType, muzzleExit.position.ToString("G4"), muzzleExit.eulerAngles.ToString("G4"));

                lastShot = Time.time;
            }
        }

        private void AimAtCenter() {
            float screenX = Screen.width / 2;
            float screenY = Screen.height / 2;

            RaycastHit hit;
            Ray ray = playerCamera.ScreenPointToRay(new Vector3(screenX, screenY));

            if (Physics.Raycast(ray, out hit))
                muzzleExit.LookAt(hit.point);
        }

        private void SwitchWeapon(object[] data) {

        }
    }
}
