namespace Dryhard.GunSystem {
    using UnityEngine;
    using Decoy.Core.EventSystem;
    using Decoy.Core.Utilities;
    using System.Collections.Generic;
    /// <summary>
    /// ProjectileController.cs
    /// <summary>
    /// Author: Thomas van Opstal
    /// </summary>
    public class ProjectileController : MonoBehaviour {
        public ProjectileModel[] projectileModels;

        public List<GameObject> projectilePool;
        private List<GameObject> projectilesInUse = new List<GameObject>();

        public List<GameObject> impactPool;
        private List<GameObject> impactPoolInUse = new List<GameObject>();

        private Dictionary<ProjectileTypes, ProjectileModel> projectileLookupDic = new Dictionary<ProjectileTypes, ProjectileModel>();

        private void Start() {
            EventManager.StartListening(GunEventTypes.FIRE_PROJECTILE, FireProjectile);
            EventManager.StartListening(GunEventTypes.RETURN_PROJECTILE, ReturnProjectile);
            EventManager.StartListening(GunEventTypes.RETURN_AND_REMOVE_IMPACT, RemoveAndReturnImpact);

            foreach (ProjectileModel model in projectileModels)
                projectileLookupDic.Add(model.type, model);
        }

        private void OnDestroy() {
            EventManager.StopListening(GunEventTypes.FIRE_PROJECTILE, FireProjectile);
            EventManager.StopListening(GunEventTypes.RETURN_PROJECTILE, ReturnProjectile);
            EventManager.StopListening(GunEventTypes.RETURN_AND_REMOVE_IMPACT, RemoveAndReturnImpact);
        }

        private void FireProjectile(object[] data) {
            ProjectileTypes type = (ProjectileTypes)data[0];
            Vector3 position = CustomSerializableObjects.Vector3FromString((string)data[1]);
            Vector3 rotation = CustomSerializableObjects.Vector3FromString((string)data[2]);

            GameObject projectile = projectilePool[0];
            projectilePool.Remove(projectile);
            projectilesInUse.Add(projectile);

            projectile.SetActive(true);
            projectile.transform.SetPositionAndRotation(position, Quaternion.Euler(rotation));

            //Make a new copy and fill it with requested type
            ProjectileModel tModel = (ProjectileModel)ScriptableObject.CreateInstance(typeof(ProjectileModel));
            tModel.damage = projectileLookupDic[type].damage;
            tModel.lifeTime = projectileLookupDic[type].lifeTime;
            tModel.speed = projectileLookupDic[type].speed;

            projectile.GetComponent<ProjectileView>().Model = tModel;
        }

        private void ReturnProjectile(object[] data) {
            GameObject projectile = (GameObject)data[0];

            projectilesInUse.Remove(projectile);
            projectilePool.Add(projectile);

            DrawImpact((RaycastHit)data[1]);
        }

        private void DrawImpact(RaycastHit impactPoint) {
            if (!impactPoint.transform)
                return;

            impactPool[0].transform.SetPositionAndRotation(impactPoint.point, Quaternion.identity);
            impactPool[0].transform.forward = impactPoint.normal;
            impactPool[0].SetActive(true);

            impactPoolInUse.Add(impactPool[0]);
            impactPool.RemoveAt(0);
        }

        private void RemoveAndReturnImpact(object[] data) {
            GameObject impact = (GameObject)data[0];

            impactPoolInUse.Remove(impact);
            impactPool.Add(impact);

            impact.SetActive(false);
        }
    }
}
