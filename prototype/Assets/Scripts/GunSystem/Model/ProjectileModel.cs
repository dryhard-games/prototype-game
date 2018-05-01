namespace Dryhard.GunSystem {
    using UnityEngine;
    /// <summary>
    /// ProjectileModel.cs
    /// <summary>
    /// Author: Thomas van Opstal
    /// </summary>
    [CreateAssetMenu(fileName = "Projectile", menuName = "New Projectile Model")]
    public class ProjectileModel : ScriptableObject {
        public ProjectileTypes type;
        public float speed;
        public float damage;
        public float lifeTime;
    }

    public enum ProjectileTypes {
        Bullet,
        Rocket,
        Arrow
    }
}
