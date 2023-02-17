using UnityEngine;

namespace Content.Game.Player.Scripts.Upgrades
{
    [CreateAssetMenu(fileName = "Neural", menuName = "Upgrades/Weapon/Mini Gun Upgrade", order = 1)]
    public class MiniGunUpgrade : ScriptableObject
    {
        public string UpgradeName = "Mini Gun Upgrade";
        public int AdditionalBulletsShot = 1;
        public Sprite MiniGinUpgradeSprite;
    }
}