namespace Dryhard.GunSystem {
    using UnityEngine;
    /// <summary>
    /// IHittable.cs
    /// <summary>
    /// Author: Thomas van Opstal
    /// </summary>
    public interface IHittable {
        /// <summary>
        /// Called when hit by projectile
        /// </summary>
        /// <param name="model">Projectile model includes damage etc...</param>
        /// <param name="position">world space location of the hit</param>
        void OnHit(ProjectileModel model, Vector3 position);
    }
}
