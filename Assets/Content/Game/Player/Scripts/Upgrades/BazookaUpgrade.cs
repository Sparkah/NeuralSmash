using UnityEngine;

namespace Content.Game.Player.Scripts.Upgrades
{
    [CreateAssetMenu(fileName = "Neural", menuName = "Upgrades/Weapon/Bazooka Upgrade", order = 1)]
    public class BazookaUpgrade : ScriptableObject
    {
        public string UpgradeName = "Bazooka Upgrade";
        public int ReloadSpeedDecrease = 2;
        public Sprite BazookaUpgradeSprite;
    }
}