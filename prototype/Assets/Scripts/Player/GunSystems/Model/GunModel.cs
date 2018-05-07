namespace Dryhard.Player.GunSystem {
    using UnityEngine;
    using Dryhard.GunSystem;
    /// <summary>
    /// GunModel.cs
    /// <summary>
    /// Author: Thomas van Opstal
    /// </summary>
    [CreateAssetMenu(fileName = "GunModel", menuName = "Dryhard/New GunModel Model")]
    public class GunModel : ScriptableObject {
        public GunTypes gunType;
        [Header("Ammo Settings")]
        public int magazineSize;
        public int magazineCount;
        public ProjectileTypes bulletType;
        [Header("Gun Settings")]
        public float rateOfFire;
        public float spreadRange;
        public float reloadSpeed;
    }

    public enum GunTypes {
        Thompson
    }
}
