using UnityEngine;

namespace Content.Game.Player.Scripts.Upgrades
{
    [CreateAssetMenu(fileName = "Neural", menuName = "Upgrades/Enabler/Spikes Gun Enabler", order = 1)]
    public class SpikesGunEnabler : ScriptableObject
    {
        public string UpgradeName = "Spikes Gun Enabler";
        public bool SpikesGunEnabled = true;
        public Sprite SpikesGunEnablerSprite;
    }
}