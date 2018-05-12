namespace Dryhard.Player.GunSystem {
    using UnityEngine;
    using Decoy.Core.InputSystem;
    using Decoy.Core.EventSystem;
    using System.Collections;
    using Dryhard.GunSystem;
    using UnityEngine.UI;
    /// <summary>
    /// PlayerGunController.cs
    /// <summary>
    /// Author: Thomas van Opstal
    /// </summary>
    public class PlayerGunController : MonoBehaviour {
        public InputModel iModel;
        public GunModel gModel;
        public Camera playerCamera;
        public Text ammoCounter;

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
            ammoCounter.text = (isReloading) ? "Reloading" : roundsLeft + "/" + gModel.magazineSize;

            if (isReloading)
                return;

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

                EventManager.TriggerEvent(GunEventTypes.FIRE_PROJECTILE, gModel.bulletType, playerCamera.transform.position.ToString("G4"), playerCamera.transform.eulerAngles.ToString("G4"));

                lastShot = Time.time;
            }
        }

        private void SwitchWeapon(object[] data) {

        }
    }
}
