namespace Dryhard.GunSystem {
    using UnityEngine;
    using Decoy.Core.EventSystem;
    using Decoy.Core.Utilities;
    using System.Collections.Generic;
    /// <summary>
    /// GunController.cs
    /// <summary>
    /// Author: Thomas van Opstal
    /// </summary>
    public class ProjectileController : MonoBehaviour {
        public ProjectileModel[] projectileModels;

        public List<GameObject> projectilePool;
        private List<GameObject> projectilesInUse;

        private Dictionary<ProjectileTypes, ProjectileModel> projectileLookupDic = new Dictionary<ProjectileTypes, ProjectileModel>();

        private void Start() {
            EventManager.StartListening(GunEventTypes.FIRE_PROJECTILE, FireProjectile);
            EventManager.StartListening(GunEventTypes.RETURN_PROJECTILE, ReturnProjectile);

            foreach (ProjectileModel model in projectileModels)
                projectileLookupDic.Add(model.type, model);
        }

        private void OnDestroy() {
            EventManager.StopListening(GunEventTypes.FIRE_PROJECTILE, FireProjectile);
            EventManager.StopListening(GunEventTypes.RETURN_PROJECTILE, ReturnProjectile);
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
            ProjectileModel tModel = new ProjectileModel();
            tModel = projectileLookupDic[type];

            projectile.GetComponent<ProjectileView>().Model = tModel;
        }

        private void ReturnProjectile(object[] data) {
            GameObject projectile = (GameObject)data[0];

            projectilesInUse.Remove(projectile);
            projectilePool.Add(projectile);
        }
    }
}
