using UnityEngine;

namespace Content.Game.Player.Scripts.Upgrades
{
    [CreateAssetMenu(fileName = "Neural", menuName = "Upgrades/Weapon/Spikes Gun Upgrade", order = 1)]
    public class SpikesGunUpgrade : ScriptableObject
    {
        public string UpgradeName = "Spikes Gun Upgrade";
        public int IncreaseSpikesAmount = 1;
        public int AdditionalDamage = 1;
        public Sprite SpikesGunUpgradeSprite;
    }
}