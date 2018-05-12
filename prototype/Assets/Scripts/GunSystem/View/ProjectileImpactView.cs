namespace Dryhard.GunSystem {
    using System.Collections;
    using UnityEngine;
    using Decoy.Core.EventSystem;
    /// <summary>
    /// ProjectileImpactView.cs
    /// <summary>
    /// Author: Thomas van Opstal
    /// </summary>
    public class ProjectileImpactView : MonoBehaviour {
        public float duration = 1.5f;

        private void OnEnable() {
            StartCoroutine(RemoveImpact());
        }

        private void OnDestroy() {
            StopAllCoroutines();
        }

        private IEnumerator RemoveImpact() {
            yield return new WaitForSeconds(duration);
            EventManager.TriggerEvent(GunEventTypes.RETURN_AND_REMOVE_IMPACT, gameObject);
        }
    }
}
