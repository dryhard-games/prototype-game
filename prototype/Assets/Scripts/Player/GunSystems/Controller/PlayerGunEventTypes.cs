namespace Dryhard.Player.GunSystem {
    /// <summary>
    /// PlayerGunEventTypes.cs
    /// <summary>
    /// Author: Thomas van Opstal
    /// </summary>
    public class PlayerGunEventTypes {
        private const string PREFIX = "PLAYER_GUN_";

        /// <summary>
        /// Arguments - 1.(GunModel)new Gun Model;
        /// </summary>
        public const string WEAPON_SWITCH_REQUEST = PREFIX + "WEAPON_SWITCH_REQUEST";
    }
}
