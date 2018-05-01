namespace Dryhard.GunSystem {
    using UnityEngine;
    /// <summary>
    /// ProjectileModel.cs
    /// <summary>
    /// Author: Thomas van Opstal
    /// </summary>
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
