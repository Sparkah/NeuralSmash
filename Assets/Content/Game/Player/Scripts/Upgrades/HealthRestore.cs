using UnityEngine;

namespace Content.Game.Player.Scripts.Upgrades
{
    [CreateAssetMenu(fileName = "Neural", menuName = "Upgrades/Stats/Restore Health", order = 1)]
    public class HealthRestore : ScriptableObject
    {
        public string UpgradeName = "Health Restore";
        public int RestoreAmount = 10;
        public Sprite HealthRestoreSprite;
    }
}