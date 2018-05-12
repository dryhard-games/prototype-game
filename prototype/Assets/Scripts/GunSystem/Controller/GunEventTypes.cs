namespace Dryhard.GunSystem {
    /// <summary>
    /// GunEventTypes.cs
    /// <summary>
    /// Author: Thomas van Opstal
    /// </summary>
    public class GunEventTypes {
        private const string PREFIX = "GUN_SYSTEM_";

        /// <summary>
        /// Arguments - 1.(enum)ProjectileTypes 2.(Vector3)position 3.(Vector3)rotation
        /// </summary>
        public const string FIRE_PROJECTILE = PREFIX + "FIRE_PROJECTILE";

        /// <summary>
        /// Arguments - 1.(GameObject)projectile
        /// </summary>
        public const string RETURN_PROJECTILE = PREFIX + "RETURN_PROJECTILE";

        /// <summary>
        /// Arguments - 1.(GameObject)impact
        /// </summary>
        public const string RETURN_AND_REMOVE_IMPACT = PREFIX + "RETURN_AND_REMOVE_IMPACT";
    }
}
