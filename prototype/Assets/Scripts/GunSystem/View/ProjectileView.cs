namespace Dryhard.GunSystem {
    using UnityEngine;
    using System.Collections;
    using Decoy.Core.EventSystem;
    /// <summary>
    /// ProjectileView.cs
    /// <summary>
    /// Author: Thomas van Opstal
    /// </summary>
    public class ProjectileView : MonoBehaviour {
        [Header("RigidBody")]
        public Rigidbody rBody;
        [Header("Collider")]
        public Collider sphereCollider;
        public float skinWidth = 0.1f;

        private int layer;
        private RaycastHit hitInfo;

        private Vector3 previousPosition;
        private float minimumExtent;
        private float partialExtent;
        private float sqrMinimumExtent;

        private Coroutine returnProjectile;

        private ProjectileModel model;
        public ProjectileModel Model {
            get { return model; }
            set { model = value;
                InitProjectile();
            }
        }

        private void OnDisable() {
            StopAllCoroutines();
        }

        private void InitProjectile() {
            layer = 1 << LayerMask.NameToLayer("Default");

            sphereCollider.enabled = true;
            rBody.velocity = transform.forward * model.speed;
            returnProjectile = StartCoroutine(ReturnProjectile(model.lifeTime));

            minimumExtent = Mathf.Min(Mathf.Min(sphereCollider.bounds.extents.x, sphereCollider.bounds.extents.y), sphereCollider.bounds.extents.z);
            partialExtent = minimumExtent * (1.0f - skinWidth);
            sqrMinimumExtent = minimumExtent * minimumExtent;

            previousPosition = transform.position;

            //Run in the first frame aswell for extra precision.
            Vector3 movementThisStep = transform.position - previousPosition;
            float movementSqrMagnitude = movementThisStep.sqrMagnitude;

            if (movementSqrMagnitude > sqrMinimumExtent) {
                float movementMagnitude = Mathf.Sqrt(movementSqrMagnitude);

                if (Physics.Raycast(previousPosition, movementThisStep, out hitInfo, movementMagnitude, layer, QueryTriggerInteraction.Ignore)) {
                    if (!hitInfo.collider || hitInfo.transform.tag == "Projectile")
                        return;

                    transform.position = hitInfo.point - (movementThisStep / movementMagnitude) * partialExtent;

                    ObstacleDetected();
                }
            }
        }

        private void Update() {
            if (!sphereCollider.enabled)
                return;

            Vector3 movementThisStep = transform.position - previousPosition;
            float movementSqrMagnitude = movementThisStep.sqrMagnitude;

            if (movementSqrMagnitude > sqrMinimumExtent) {
                float movementMagnitude = Mathf.Sqrt(movementSqrMagnitude);

                if (Physics.Raycast(previousPosition, movementThisStep, out hitInfo, movementMagnitude, layer, QueryTriggerInteraction.Ignore)) {
                    if (!hitInfo.collider || hitInfo.transform.tag == "Projectile")
                        return;

                    transform.position = hitInfo.point - (movementThisStep / movementMagnitude) * partialExtent;

                    ObstacleDetected();
                }
            }

            previousPosition = transform.position;
        }

        private void ObstacleDetected() {
            sphereCollider.enabled = false;
            rBody.velocity = Vector3.zero;
            rBody.angularVelocity = Vector3.zero;

            if (returnProjectile != null)
                StopCoroutine(returnProjectile);

                if (hitInfo.transform != null)
                    if (hitInfo.transform.GetComponentInParent<IHittable>() != null)
                        hitInfo.transform.GetComponentInParent<IHittable>().OnHit(model, hitInfo.point);

            EventManager.TriggerEvent(GunEventTypes.RETURN_PROJECTILE, gameObject, hitInfo);

            gameObject.SetActive(false);
        }

        private IEnumerator ReturnProjectile(float time) {
            yield return new WaitForSeconds(time);

            sphereCollider.enabled = false;
            rBody.velocity = Vector3.zero;
            rBody.angularVelocity = Vector3.zero;

            EventManager.TriggerEvent(GunEventTypes.RETURN_PROJECTILE, gameObject, hitInfo);

            gameObject.SetActive(false);
        }
    }
}
