using UnityEngine;

namespace Content.Game.Player.Scripts.Upgrades
{
    [CreateAssetMenu(fileName = "Neural", menuName = "Upgrades/Weapon/Increase Gun Damage", order = 1)]
    public class GunUpgrade : ScriptableObject
    {
        public string UpgradeName = "Gun Upgrade";
        public float AdditionalDamage = 1;
        public Sprite GunUpgradeSprite;
    }
}